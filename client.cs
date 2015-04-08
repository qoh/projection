exec("./mat4.cs");
exec("./test.cs");

function getProjectionMatrix() {
    %transform = ServerConnection.getControlObject().getTransform();
    %resInfo = getRes();

    // Bring everything into space relative to the camera (modelview matrix)
    %view = mat4::identity();
    %view = mat4::rotate(%view, getWords(%transform, 3, 5), getWord(%transform, 6));
    %view = mat4::translate(%view, vectorScale(%transform, -1));
    %view = mat4::mul_mat("1 0 0 0" SPC "0 0 -1 0" SPC "0 1 0 0" SPC "0 0 0 1",
                          %view);
    // Perspective projection matrix
    %proj = mat4::perspective(
        // Aspect ratio
        getWord(%resInfo, 0) / getWord(%resInfo, 1),
        // Field of view in radians
        mDegToRad(ServerConnection.getControlCameraFov()),
        // Near and far planes
        $pref::TS::screenError, $Pref::visibleDistanceMax
    );

    // Combine them
    return mat4::mul_mat(%proj, %view);
}

// Project 3D world coordinates as a 4D vector into 3D screen coordinates
function projectWorldToScreen(%world) {
    // Run world point through projection matrix
    return mat4::mul_vec(getProjectionMatrix(), %world);
}

// Project 3D screen coordinates as a 4D vector into 3D world coordinates
function projectScreenToWorld(%screen) {
    // Run screen point through inverse of projection matrix
    return mat4::mul_vec(mat4::inverse(getProjectionMatrix()), %screen);
}
