import { WindowEvents } from "@drozdik.m/window-events";
import { Circle } from "../Menu/Circle";


let circles: Circle[][] = []


/**
 * Callback function interface
 * */
interface CircleWaveCallback
{
    (circles: Circle[], step: number): void
}

/**
 * Method that iterates over circles in a wave-like fashion
 * @param xStart Start x coordinate
 * @param yStart Start y coordinate
 * @param callback Callback function
 * @param stepLimit Step limit (undefined for the whole board)
 */
function StartCircleWave(xStart: number, yStart: number, callback: CircleWaveCallback, stepLimit: number = undefined)
{
    //Get bounds
    let boundX = circles.length;
    let boundY = boundX == 0 ? 0 : circles[0].length
    function InBounds(x: number, y: number): boolean
    {
        if (x < 0 || y < 0)
            return false;
        if (x >= boundX || y >= boundY)
            return false;
        return true;
    }

    //Starting point
    callback([circles[xStart][yStart]], 0);

    //Waves
    let wave = 1
    let waveLimit = typeof(stepLimit) === "undefined"
        ? Math.max(xStart, yStart, boundX - xStart, boundY - yStart)
        : stepLimit
    while (wave <= waveLimit)
    {
        let currentWaveCircles: Circle[] = []

        //Top border
        // * * *
        // - - -
        // - - -
        for (let x = xStart - wave, y = Math.max(yStart - wave, 0);
            y <= yStart + wave && x >= 0 && y < boundY;
            y++)
            currentWaveCircles.push(circles[x][y]);
        
        //bottom border
        // - - -
        // - - -
        // * * *
        for (let x = xStart + wave, y = Math.max(yStart - wave, 0);
            y <= yStart + wave && x < boundX && y < boundY;
            y++)
            currentWaveCircles.push(circles[x][y]);

        //left border
        // - - -
        // * - -
        // - - -
        for (let x = Math.max(xStart - wave, 0), y = yStart - wave;
            x <= xStart + wave && y >= 0 && x < boundX;
            x++)
            currentWaveCircles.push(circles[x][y]);

        //right border
        // - - -
        // * - -
        // - - -
        for (let x = Math.max(xStart - wave, 0), y = yStart + wave;
            x <= xStart + wave && y < boundY && x < boundX;
            x++)
            currentWaveCircles.push(circles[x][y]);

        //Check if any new circles
        if (currentWaveCircles.length <= 0)
            break;

        //Finish this loop
        callback(currentWaveCircles, wave);
        wave += 1;
    }
    
}

WindowEvents.OnDOMReady.Add(function ()
{
    let contentElement = document.getElementById("heroHeaderContent");
    let circlesSVGElement = document.getElementById("heroHeaderCircles");

    //--- Establish all circles ---
    let circleColumnCount = 6;
    let circleRowCount = 12;
    let circlesSVG = "";

    //Get SVG
    for (let x = 0; x < circleColumnCount; x++)
        for (let y = 0; y < circleRowCount; y++)
            circlesSVG += Circle.GetSVG(x, y, 0);

    //Set HTML
    circlesSVGElement.innerHTML = circlesSVG;

    //Create circle objects
    for (let x = 0; x < circleColumnCount; x++)
    {
        circles[x] = [];
        for (let y = 0; y < circleRowCount; y++)
            circles[x][y] = new Circle(x, y);
    }

    /**
     * Wave callback that creates basic opacity wave
     * */
    function CircleWaveCallback(circles: Circle[], step: number): void
    {
        setTimeout(function ()
        {
            for (let i = 0; i < circles.length; i++)
            {
                circles[i].SetRadius(20);
            }
                
        }, 100 * step);

        setTimeout(function ()
        {
            for (let i = 0; i < circles.length; i++)
            {
                circles[i].SetRandomOpacity();
                circles[i].SetRadius(Circle.radius);
            }
        }, 130 * step);
    }

    //Initial circle draw
    let waveRootX = 5;
    let waveRootY = 9;
    StartCircleWave(waveRootX, waveRootY, CircleWaveCallback);

    //Wave on circle click
    for (let x = 0; x < circleColumnCount; x++)
    {
        for (let y = 0; y < circleRowCount; y++)
        {
            circles[x][y].element.addEventListener("click", function ()
            {
                StartCircleWave(x, y, CircleWaveCallback);
            });
        }
    }
});