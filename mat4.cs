function mat4::identity() {
    return "1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1";
}

function mat4::mul_vec(%m, %v) {
    %x = getWord(%v, 0);
    %y = getWord(%v, 1);
    %z = getWord(%v, 2);
    %w = getWord(%v, 3);

    return
        getWord(%m,  0) * %x + getWord(%m,  1) * %y + getWord(%m,  2) * %z + getWord(%m,  3) * %w SPC
        getWord(%m,  4) * %x + getWord(%m,  5) * %y + getWord(%m,  6) * %z + getWord(%m,  7) * %w SPC
        getWord(%m,  8) * %x + getWord(%m,  9) * %y + getWord(%m, 10) * %z + getWord(%m, 11) * %w SPC
        getWord(%m, 12) * %x + getWord(%m, 13) * %y + getWord(%m, 14) * %z + getWord(%m, 15) * %w;
}

function mat4::mul_mat(%m, %n) {
    %a00 = getWord(%a,  0); %a01 = getWord(%a,  1); %a02 = getWord(%a,  2); %a03 = getWord(%a,  3);
    %a10 = getWord(%a,  4); %a11 = getWord(%a,  5); %a12 = getWord(%a,  6); %a13 = getWord(%a,  7);
    %a20 = getWord(%a,  8); %a21 = getWord(%a,  9); %a22 = getWord(%a, 10); %a23 = getWord(%a, 11);
    %a30 = getWord(%a, 12); %a31 = getWord(%a, 13); %a32 = getWord(%a, 14); %a33 = getWord(%a, 15);

    %b00 = getWord(%b,  0); %b01 = getWord(%b,  1); %b02 = getWord(%b,  2); %b03 = getWord(%b,  3);
    %b10 = getWord(%b,  4); %b11 = getWord(%b,  5); %b12 = getWord(%b,  6); %b13 = getWord(%b,  7);
    %b20 = getWord(%b,  8); %b21 = getWord(%b,  9); %b22 = getWord(%b, 10); %b23 = getWord(%b, 11);
    %b30 = getWord(%b, 12); %b31 = getWord(%b, 13); %b32 = getWord(%b, 14); %b33 = getWord(%b, 15);

    return
        %b00*%a00 + %b01*%a10 + %b02*%a20 + %b03*%a30 SPC
        %b00*%a01 + %b01*%a11 + %b02*%a21 + %b03*%a31 SPC
        %b00*%a02 + %b01*%a12 + %b02*%a22 + %b03*%a32 SPC
        %b00*%a03 + %b01*%a13 + %b02*%a23 + %b03*%a33 SPC

        %b10*%a00 + %b11*%a10 + %b12*%a20 + %b13*%a30 SPC
        %b10*%a01 + %b11*%a11 + %b12*%a21 + %b13*%a31 SPC
        %b10*%a02 + %b11*%a12 + %b12*%a22 + %b13*%a32 SPC
        %b10*%a03 + %b11*%a13 + %b12*%a23 + %b13*%a33 SPC

        %b20*%a00 + %b21*%a10 + %b22*%a20 + %b23*%a30 SPC
        %b20*%a01 + %b21*%a11 + %b22*%a21 + %b23*%a31 SPC
        %b20*%a02 + %b21*%a12 + %b22*%a22 + %b23*%a32 SPC
        %b20*%a03 + %b21*%a13 + %b22*%a23 + %b23*%a33 SPC

        %b30*%a00 + %b31*%a10 + %b32*%a20 + %b33*%a30 SPC
        %b30*%a01 + %b31*%a11 + %b32*%a21 + %b33*%a31 SPC
        %b30*%a02 + %b31*%a12 + %b32*%a22 + %b33*%a32 SPC
        %b30*%a03 + %b31*%a13 + %b32*%a23 + %b33*%a33;
}

function mat4::inverse(%m) {
}

function mat4::translate(%m, %v) {
    %x = getWord(%v, 0);
    %y = getWord(%v, 1);
    %z = getWord(%v, 2);

    return getWords(%m, 0, 11) SPC
        getWord(%a, 0) * %x + getWord(%a, 4) * %y + getWord(%a,  8) * %z + getWord(%a, 12) SPC
        getWord(%a, 1) * %x + getWord(%a, 5) * %y + getWord(%a,  9) * %z + getWord(%a, 13) SPC
        getWord(%a, 2) * %x + getWord(%a, 6) * %y + getWord(%a, 10) * %z + getWord(%a, 14) SPC
        getWord(%a, 3) * %x + getWord(%a, 7) * %y + getWord(%a, 11) * %z + getWord(%a, 15);
}

function mat4::rotate(%m, %axis, %rads) {
}

function mat4::project(%aspect, %field, %near, %far) {
    %f = 1 / mTan(%fov / 2);
    %nf = 1 / (%near - %far);
    return
        %f / %aspect SPC "0 0 0" SPC
        "0" SPC %f SPC "0 0" SPC
        "0 0" SPC (%far + %near) * %nf SPC "-1" SPC
        "0 0" SPC (2 * %far * %near) * %nf SPC "0";
}
