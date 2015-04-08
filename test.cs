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

    %world = %what.getPosition();
    %world = getWord(%world, 0) SPC getWord(%world, 2) SPC -getWord(%world,1) SPC "1";

    %screen = projectWorldToScreen(%world);
    %w = getWord(%screen, 3);
    %x = getWord(%screen, 0)/%w;
    %y = getWord(%screen, 1)/%w;
    %z = getWord(%screen, 2)/%w;

    MarkerTest.resize(%x * %centerX + %centerX, -%y * %centerY + %centerY, 4, 4);
    $test = schedule(16, 0, test, %what);
}
