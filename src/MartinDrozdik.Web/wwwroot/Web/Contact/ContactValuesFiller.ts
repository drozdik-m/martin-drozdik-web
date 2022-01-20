import { WindowEvents } from "@drozdik.m/window-events";

/*
This is for confusing automatic scrappers for emails and phone numbers. It is effective against scrappers that don't run JS.
 */


function FillPhoneNumbers()
{
    let numberParts = [
        "+420",
        "737",
        "416",
        "525"
    ]
    let myPhoneNumberSquished = numberParts[0] + numberParts[1] + numberParts[2] + numberParts[3]
    let myPhoneNumber = `${numberParts[0]} ${numberParts[1]} ${numberParts[2]} ${numberParts[3]}`

    let phones = document.querySelectorAll("[data-phone-link]")
    for (let i = 0; i < phones.length; i++)
    {
        phones[i].setAttribute("href", "tel:" + myPhoneNumberSquished);
        phones[i].textContent = myPhoneNumber;
    }
}

function FillEmails()
{
    let emailParts = [
        "ja",
        "@",
        "martin-drozdik",
        ".cz"
    ]
    let myEmail = emailParts[0] + emailParts[1] + emailParts[2] + emailParts[3]

    let emails = document.querySelectorAll("[data-email-link]")
    for (let i = 0; i < emails.length; i++)
    {
        emails[i].setAttribute("href", "mailto:" + myEmail);
        emails[i].textContent = myEmail;
    }
}


WindowEvents.OnDOMReady.Add(function ()
{
    FillPhoneNumbers();
    FillEmails();
});