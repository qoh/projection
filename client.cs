exec("./mat4.cs");
exec("./test.cs");

function getProjectionMatrix() {
    %transform = ServerConnection.getControlObject().getTransform();
    %resInfo = getRes();

    %matRotate    = mat4::rotate(mat4::identity(), getWords(%transform, 3, 5), getWord(%transform, 6));
    %matTranslate = mat4::translate(mat4::identity(), vectorScale(%transform, -1));
    %view = mat4::mul_mat(%matRotate, %matTranslate);
    
    %proj = mat4::perspective(
        getWord(%resInfo, 0) / getWord(%resInfo, 1),
        mDegToRad(ServerConnection.getControlCameraFov()),
        $pref::TS::screenError,
        $Pref::visibleDistanceMax
    );

    return mat4::mul_mat(%proj, %view);
}

function projectWorldToScreen(%world) {
    return mat4::mul_vec(getProjectionMatrix(), %world);
}

function projectScreenToWorld(%screen) {
    return mat4::mul_vec(mat4::inverse(getProjectionMatrix()), %screen);
}
