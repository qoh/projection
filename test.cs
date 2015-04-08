if (!isObject(MarkerTest)) {
    new GuiSwatchCtrl(MarkerTest);
    PlayGui.add(MarkerTest);
    MarkerTest.resize(0, 0, 4, 4);
    MarkerTest.setColor("1 0 1 1");
}

function test(%what) {
    cancel($test);

    %res = getRes();
    %resW = getWord(%res, 0);
    %resH = getWord(%res, 1);
    %centerX = %resW / 2;
    %centerY = %resH / 2;

    %screen = projectWorldToScreen(%what.getPosition() SPC 1);
    %x = %centerX + getWord(%screen, 0) / 3 * %centerX;
    %y = %centerY + getWord(%screen, 2) / 3 * %centerY;

    MarkerTest.resize(%x, %y, 4, 4);
    $test = schedule(60, 0, test, %what);
}
