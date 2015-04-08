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
    %a00 = getWord(%a,  0); %a01 = getWord(%a,  1); %a02 = getWord(%a,  2); %a03 = getWord(%a,  3);
    %a10 = getWord(%a,  4); %a11 = getWord(%a,  5); %a12 = getWord(%a,  6); %a13 = getWord(%a,  7);
    %a20 = getWord(%a,  8); %a21 = getWord(%a,  9); %a22 = getWord(%a, 10); %a23 = getWord(%a, 11);
    %a30 = getWord(%a, 12); %a31 = getWord(%a, 13); %a32 = getWord(%a, 14); %a33 = getWord(%a, 15);

    %b00 = %a00 * %a11 - %a01 * %a10;
    %b01 = %a00 * %a12 - %a02 * %a10;
    %b02 = %a00 * %a13 - %a03 * %a10;
    %b03 = %a01 * %a12 - %a02 * %a11;
    %b04 = %a01 * %a13 - %a03 * %a11;
    %b05 = %a02 * %a13 - %a03 * %a12;
    %b06 = %a20 * %a31 - %a21 * %a30;
    %b07 = %a20 * %a32 - %a22 * %a30;
    %b08 = %a20 * %a33 - %a23 * %a30;
    %b09 = %a21 * %a32 - %a22 * %a31;
    %b10 = %a21 * %a33 - %a23 * %a31;
    %b11 = %a22 * %a33 - %a23 * %a32;

    %det = %b00 * %b11 - %b01 * %b10 + %b02 * %b09 + %b03 * %b08 - %b04 * %b07 + %b05 * %b06;
    
    if (%det == 0) {
        return "";
    }
    
    %det = 1 / %det;

    return
        (%a11 * %b11 - %a12 * %b10 + %a13 * %b09) * %det SPC
        (%a02 * %b10 - %a01 * %b11 - %a03 * %b09) * %det SPC
        (%a31 * %b05 - %a32 * %b04 + %a33 * %b03) * %det SPC
        (%a22 * %b04 - %a21 * %b05 - %a23 * %b03) * %det SPC
        (%a12 * %b08 - %a10 * %b11 - %a13 * %b07) * %det SPC
        (%a00 * %b11 - %a02 * %b08 + %a03 * %b07) * %det SPC
        (%a32 * %b02 - %a30 * %b05 - %a33 * %b01) * %det SPC
        (%a20 * %b05 - %a22 * %b02 + %a23 * %b01) * %det SPC
        (%a10 * %b10 - %a11 * %b08 + %a13 * %b06) * %det SPC
        (%a01 * %b08 - %a00 * %b10 - %a03 * %b06) * %det SPC
        (%a30 * %b04 - %a31 * %b02 + %a33 * %b00) * %det SPC
        (%a21 * %b02 - %a20 * %b04 - %a23 * %b00) * %det SPC
        (%a11 * %b07 - %a10 * %b09 - %a12 * %b06) * %det SPC
        (%a00 * %b09 - %a01 * %b07 + %a02 * %b06) * %det SPC
        (%a31 * %b01 - %a30 * %b03 - %a32 * %b00) * %det SPC
        (%a20 * %b03 - %a21 * %b01 + %a22 * %b00) * %det;
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

function mat4::rotate(%a, %axis, %rads) {
    %x = getWord(%axis, 0);
    %y = getWord(%axis, 1);
    %z = getWord(%axis, 2);
    %len = vectorLen(%axis);
    
    if (%len == 0) {
        return %a;
    }

    %len = 1 / %len;
    %x *= %len;
    %y *= %len;
    %z *= %len;

    %s = mSin(%rads);
    %c = mCos(%rads);
    %t = 1 - %c;

    %a00 = getWord(%a, 0); %a01 = getWord(%a, 1); %a02 = getWord(%a,  2); %a03 = getWord(%a,  3);
    %a10 = getWord(%a, 4); %a11 = getWord(%a, 5); %a12 = getWord(%a,  6); %a13 = getWord(%a,  7);
    %a20 = getWord(%a, 8); %a21 = getWord(%a, 9); %a22 = getWord(%a, 10); %a23 = getWord(%a, 11);

    %b00 = %x * %x * %t + %c;      %b01 = %y * %x * %t + %z * %s; %b02 = %z * %x * %t - %y * %s;
    %b10 = %x * %y * %t - %z * %s; %b11 = %y * %y * %t + %c;      %b12 = %z * %y * %t + %x * %s;
    %b20 = %x * %z * %t + %y * %s; %b21 = %y * %z * %t - %x * %s; %b22 = %z * %z * %t + %c;

    return
        %a00 * %b00 + %a10 * %b01 + %a20 * %b02 SPC
        %a01 * %b00 + %a11 * %b01 + %a21 * %b02 SPC
        %a02 * %b00 + %a12 * %b01 + %a22 * %b02 SPC
        %a03 * %b00 + %a13 * %b01 + %a23 * %b02 SPC
        %a00 * %b10 + %a10 * %b11 + %a20 * %b12 SPC
        %a01 * %b10 + %a11 * %b11 + %a21 * %b12 SPC
        %a02 * %b10 + %a12 * %b11 + %a22 * %b12 SPC
        %a03 * %b10 + %a13 * %b11 + %a23 * %b12 SPC
        %a00 * %b20 + %a10 * %b21 + %a20 * %b22 SPC
        %a01 * %b20 + %a11 * %b21 + %a21 * %b22 SPC
        %a02 * %b20 + %a12 * %b21 + %a22 * %b22 SPC
        %a03 * %b20 + %a13 * %b21 + %a23 * %b22 SPC
        getWords(%a, 12, 15);
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
