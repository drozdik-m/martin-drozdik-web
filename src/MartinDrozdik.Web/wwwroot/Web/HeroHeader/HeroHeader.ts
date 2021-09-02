import { WindowEvents } from "@drozdik.m/window-events";
import { Circle } from "../Menu/Circle";


let circles: Circle[][] = []


interface CircleWaveCallback
{
    (circles: Circle[], step: number): void
}

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
    let waveLimit = stepLimit == undefined
        ? Math.max(xStart, yStart, boundX - xStart, boundY - yStart)
        : stepLimit
    while (wave <= waveLimit)
    {
        let currentWaveCircles: Circle[] = []

        //Top border
        // * * *
        // - - -
        // - - -
        for (let x = xStart + wave, y = yStart - wave; y < yStart + wave; y++)
            currentWaveCircles.push(circles[x][y]);
        
        /*//bottom border
        // - - -
        // - - -
        // * * *
        for (let x = xStart - wave, y = yStart - wave; y < yStart + wave; y++)
            currentWaveCircles.push(circles[x][y]);

        //left border
        // - - -
        // * - -
        // - - -
        for (let x = xStart + wave, y = yStart + wave + 1; x < xStart + wave - 1; x++)
            currentWaveCircles.push(circles[x][y]);

        //right border
        // - - -
        // * - -
        // - - -
        for (let x = xStart - wave, y = yStart + wave + 1; x < xStart + wave - 1; x++)
            currentWaveCircles.push(circles[x][y]);*/

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
    let circleXCount = 12;
    let circleYCount = 6;
    let circlesSVG = "";

    //Get SVG
    for (let x = 0; x < circleXCount; x++)
        for (let y = 0; y < circleYCount; y++)
            circlesSVG += Circle.GetSVG(x, y, 0.25);

    //Set HTML
    circlesSVGElement.innerHTML = circlesSVG;

    //Create circle objects
    for (let x = 0; x < circleXCount; x++)
    {
        circles[x] = [];
        for (let y = 0; y < circleYCount; y++)
            circles[x][y] = new Circle(x, y);
    }
        

    //Initial circle draw
    StartCircleWave(2, 5, function (circles: Circle[], step: number): void
    {
        console.log(circles);
        for (let i = 0; i < circles.length; i++)
            circles[i].SetOpacity(1);
    }, 0);
});