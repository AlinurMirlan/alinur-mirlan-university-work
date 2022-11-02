public enum ColorEnum {
    RED(new int[] { 255, 0, 0 }),
    YELLOW(new int[] { 255, 255, 0 }),
    ORANGE(new int[] { 255, 189, 0 }),
    GREEN(new int[] { 0, 255, 0 }),
    JADE(new int[] { 255, 87, 51 }),
    CRIMSON(new int[] { 220, 20, 60 }),
    BLUE(new int[] { 0, 0, 255 }),
    TURQUOISE(new int[] { 0, 255, 151 }),
    PINK(new int[] { 255, 0, 255 }),
    PURPLE(new int[] { 152, 0, 223 });

    public int[] rgbValues;
    ColorEnum(int[] rgbValues) {
        this.rgbValues = rgbValues;
    }
}
