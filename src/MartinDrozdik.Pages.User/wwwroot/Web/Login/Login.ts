import { WindowEvents } from "@drozdik.m/window-events";
import { Form } from "@drozdik.m/form";
import { NativeFormInput } from "@drozdik.m/form/dist/inputs/NativeFormInput";
import { FormValidation_MustBeEmail } from "@drozdik.m/form/dist/validation/FormValidation_MustBeEmail";
import { FormValidation_MustHaveValue } from "@drozdik.m/form/dist/validation/FormValidation_MustHaveValue";

declare let lang_emailWrongFormat: string;
declare let lang_emailNotEmpty: string;
declare let lang_passwordNotEmpty: string;

WindowEvents.OnDOMReady.Add(function ()
{
    let form = new Form("loginForm");

    let email = new NativeFormInput(document.getElementById("email"));
    email.AddValidation(new FormValidation_MustHaveValue(email, lang_emailNotEmpty));
    email.AddValidation(new FormValidation_MustBeEmail(email, lang_emailWrongFormat));

    let password = new NativeFormInput(document.getElementById("password"));
    password.AddValidation(new FormValidation_MustHaveValue(password, lang_passwordNotEmpty));

    form.AddInput(email);
    form.AddInput(password);

    form.OnValidSubmit.Add(function ()
    {
        form.Submit();
    });
});