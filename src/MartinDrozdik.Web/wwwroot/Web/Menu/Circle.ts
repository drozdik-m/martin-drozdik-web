
export class Circle
{
    static opacities = [0.0175, 0.05, 0.1, 0.25]
    static step = 175;
    static radius = 45;
    static offset = 30;

    x: number;
    y: number;
    element: HTMLElement


    constructor(x: number, y: number)
    {
        this.x = x;
        this.y = y;
        this.element = document.getElementById(Circle.GetId(x, y));
    }

    /**
     * Sets opacity of this circle to a new number
     * @param newOpacity
     */
    SetOpacity(newOpacity: number)
    {
        this.element.setAttribute("fill-opacity", newOpacity.toString());
    }

    /**
     * Sets random opacity to this circle
     * */
    SetRandomOpacity()
    {
        let newOpacitry = Circle.opacities[this.GetRandomInt(Circle.opacities.length)];
        this.SetOpacity(newOpacitry);
    }

    /**
     * Returns random number from 0 to max (excluding)
     * @param max The max number (excluding)
     */
    private GetRandomInt(max: number): number
    {
        return Math.floor(Math.random() * max);
    }


    /**
     * Returns SVG code for a circle
     * @param x The x index of the circle
     * @param y The y index of the circle
     * @param opacity
     */
    static GetSVG(x: number, y: number, opacity: number): string
    {
        let cx = this.step * x + this.radius + this.offset;
        let cy = this.step * y + this.radius + this.offset;
        return `<circle id="${Circle.GetId(x, y)}" cx="${cx}" cy="${cy}" r="${this.radius}" fill="white" fill-opacity="${opacity}" />`;
    }

    /**
     * Returns circle id based on its coordinates
     * @param x
     * @param y
     */
    static GetId(x: number, y: number): string
    {
        return `headerCircle${x}-${y}`
    }
}
