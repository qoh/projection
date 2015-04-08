exec("./mat4.cs");

function getProjectionMatrix() {
    %transform = ServerConnection.getControlObject.getTransform();
    %resInfo = getRes();
    
    %view = mat4::identity();
    %view = mat4::translate(%view, getWords(%transform, 0, 2));
    %view = mat4::rotate(%view, getWords(%transform, 3, 5), getWord(%transform, 6));

    %proj = mat4::perspective(
        getWord(%resInfo, 0) / getWord(%resInfo, 1),
        ServerConnection.getControlCameraFov(),
        $pref::TS::screenError,
        $Pref::visibleDistanceMax
    );

    // Is this in the right order?
    return mat4::mul_mat(%view, %proj);
}

function projectWorldToScreen(%world) {
    return mat4::mul_vec(getProjectionMatrix(), %world);
}

function projectScreenToWorld(%screen) {
    return mat4::mul_vec(mat4::inverse(getProjectionMatrix()), %screen);
}
