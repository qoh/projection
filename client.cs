exec("./mat4.cs");

function getProjectionMatrix() {
    %transform = ServerConnection.getControlObject.getTransform();
    %view = mat4::identity();
    %view = mat4::translate(%view, getWords(%transform, 0, 2));
    %view = mat4::rotate(%view, getWords(%transform, 3, 5), getWord(%transform, 5));

    %proj = mat4::perspective(
        getWord(%resInfo, 0) / getWord(%resInfo, 1),
        ServerConnection.getControlCameraFov(),
        "need to find near plane",
        "need to find view distance"
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
