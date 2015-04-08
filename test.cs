if (!isObject(MarkerTest)) {
    new GuiSwatchCtrl(MarkerTest);
    PlayGui.add(MarkerTest);
    MarkerTest.resize(0, 0, 4, 4);
    MarkerTest.setColor("1 0 1 1");
}

function test(%what) {
    cancel($test);

    %res = getRes();
    %centerX = getWord(%res, 0) / 2;
    %centerY = getWord(%res, 1) / 2;

    %screen = projectWorldToScreen(%what.getPosition() SPC 1);
    // Why are these coordinates in XZY order!?
    %x = %centerX + getWord(%screen, 0) / $pi * %centerX;
    %z = getWord(%screen, 1);
    %y = %centerY + getWord(%screen, 2) / $pi * %centerY;

    MarkerTest.resize(%x, %y, 4, 4);
    $test = schedule(16, 0, test, %what);
}
