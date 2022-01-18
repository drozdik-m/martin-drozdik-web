import { WindowEvents } from "@drozdik.m/window-events";
import { Form } from "@drozdik.m/form";
import { NativeFormInput } from "@drozdik.m/form/dist/inputs/NativeFormInput";
import { CaptchaFormInput } from "@drozdik.m/form/dist/inputs/CaptchaFormInput";
import { FormValidation_MustBeEmail } from "@drozdik.m/form/dist/validation/FormValidation_MustBeEmail";
import { FormValidation_MustHaveValue } from "@drozdik.m/form/dist/validation/FormValidation_MustHaveValue";
import { FormValidation_Captcha } from "@drozdik.m/form/dist/validation/FormValidation_Captcha";
import { FormValidation_MaxLength } from "@drozdik.m/form/dist/validation/FormValidation_MaxLength";
import { DialogWindow } from "@drozdik.m/dialog-window";
import { RecaptchaV2 } from "@drozdik.m/recaptcha";
import { Ajax, AjaxParameter, AjaxResponse } from "@drozdik.m/ajax";
import { Pipeline } from "@drozdik.m/pipeline";
import { LoadingAnimation } from "@drozdik.m/loading-animation";
import { StatusWindowSuccess } from "../StatusWindow/StatusWindowSuccess";
import { StatusWindowError } from "../StatusWindow/StatusWindowError";

WindowEvents.OnDOMReady.Add(function ()
{
    //RECAPTCHA DIALOG
    let recaptchaDialog = new DialogWindow("contactFormRecaptchaDialogWindow")

    //FORM
    let form = new Form("contactFormElement");

    let name = new NativeFormInput(document.getElementById("contactNameAndSurname"));
    name.AddValidation(new FormValidation_MustHaveValue(name, "Jméno musí být vyplněno"));
    name.AddValidation(new FormValidation_MaxLength(name, "Jméno může být dlouhé maximálně 256 znaků", 256));

    let email = new NativeFormInput(document.getElementById("contactEmail"));
    email.AddValidation(new FormValidation_MustHaveValue(email, "Email musí být vyplněn"));
    email.AddValidation(new FormValidation_MustBeEmail(email, "Email musí mít formát xx@yy.zz"));
    email.AddValidation(new FormValidation_MaxLength(email, "Email může být dlouhý maximálně 128 znaků", 128));

    let subject = new NativeFormInput(document.getElementById("contactSubject"));

    let message = new NativeFormInput(document.getElementById("contactMessage"));
    message.AddValidation(new FormValidation_MustHaveValue(message, "Zpráva musí být vyplněna"));
    email.AddValidation(new FormValidation_MaxLength(email, "Zpráva může být dlouhá maximálně 3072 znaků", 3072));

    let recaptcha = new RecaptchaV2("contactFormRecaptcha", "6LdXQBIeAAAAAJcKBXX0pooRowMHxA2osrkNbtvt");
    let recaptchaInput = new CaptchaFormInput(document.getElementById("contactFormRecaptcha"), recaptcha);
    recaptchaInput.AddValidation(new FormValidation_Captcha(recaptchaInput, ""));

    form.AddInput(name);
    form.AddInput(email);
    form.AddInput(subject);
    form.AddInput(message);
    form.AddInput(recaptchaInput);

    function SubmitForm()
    {
        let ajax = new Ajax();
        LoadingAnimation.Show();
        ajax.Post("/Home/SendMail", [
            new AjaxParameter("Name", name.Value()),
            new AjaxParameter("Email", email.Value()),
            new AjaxParameter("Subject", subject.Value()),
            new AjaxParameter("Message", message.Value()),
            new AjaxParameter("RecaptchaResponse", recaptcha.GetResponse()),
        ]).Then(function (response: AjaxResponse)
        {
            //Check for response error
            if (response.IsError())
                return Pipeline.Reject(new Error(response.StatusText() + ": " + response.Response()))

            return response.Response();
        }).Then(function (response: string)
        {
            //Handle success response
            LoadingAnimation.Hide();
            let successDialog = new StatusWindowSuccess("contactFormSuccessDialog",
                {
                    heading: "Zpráva úspěšně odeslána",
                    message: "Odpovím vám, jakmile to bude možné",
                    closeButtonText: "Zavřít"
                });
            successDialog.Display();
            form.Reset();
        }).Catch(function (error: Error)
        {
            console.error(error);
            LoadingAnimation.Hide();
            let errorDialog = new StatusWindowError("contactFormErrorDialog",
                {
                    heading: "Odeslání selhalo",
                    message: "Něco se moc pokazilo :( <br /> " + error.message,
                    closeButtonText: "Zavřít"
                });
            errorDialog.Display();
        });

    }

    recaptcha.OnValidate.Add(function ()
    {
        recaptchaDialog.Close();
        if (form.Validate())
            SubmitForm();
    });

    form.OnInvalidSubmit.Add(function ()
    {
        if (name.IsValid() && email.IsValid() && subject.IsValid() && message.IsValid())
            recaptchaDialog.Open();
    });

    form.OnValidSubmit.Add(function ()
    {
        SubmitForm();
    });
});