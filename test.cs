if (!isObject(TestMarker)) {
    new GuiSwatchCtrl(TestMarker) {
        profile = "GuiDefaultProfile";
        visible = 0;
        color = "0 0 0 0";
        minExtent = "0 0";
        extent = "19 19";

        new GuiSwatchCtrl() {
            profile = "GuiDefaultProfile";
            color = "255 0 255 255";
            minExtent = "0 0";
            position = "0 9";
            extent = "19 1";
        };

        new GuiSwatchCtrl() {
            profile = "GuiDefaultProfile";
            color = "255 0 255 255";
            minExtent = "0 0";
            position = "9 0";
            extent = "1 19";
        };
    };

    PlayGui.add(TestMarker);
}

function test(%what) {
    cancel($test);

    %res = getRes();
    %centerX = getWord(%res, 0) / 2;
    %centerY = getWord(%res, 1) / 2;

    %world = %what.getPosition();
    // %world = findLocalClient().player.getMuzzlePoint(0);
    %world = getWord(%world, 0) SPC getWord(%world, 2) SPC -getWord(%world, 1) SPC "1";

    %screen = projectWorldToScreen(%world);
    %w = getWord(%screen, 3);
    %x = getWord(%screen, 0) / %w;
    %y = getWord(%screen, 1) / %w;
    %z = getWord(%screen, 2) / %w;

    // Linearize the depth
    %f = $Pref::visibleDistanceMax;
    %n = $pref::TS::screenError;
    %z = (2 * %n) / (%f + %n - %z * (%f - %n));

    clientCmdBottomPrint("\c6NDC: " @ %x SPC %y SPC %z @ "\n", 1, true);

    TestMarker.resize(
         %x * %centerX + %centerX - 9,
        -%y * %centerY + %centerY - 9,
        19, 19);
    TestMarker.setVisible(%z > -1 && %z < 1);

    $test = schedule(16, 0, test, %what);
}
