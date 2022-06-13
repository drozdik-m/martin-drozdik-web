import { WindowEvents } from "@drozdik.m/window-events";
import { Form } from "@drozdik.m/form";
import { NativeFormInput } from "@drozdik.m/form/dist/inputs/NativeFormInput";
import { FormValidation_MustBeEmail } from "@drozdik.m/form/dist/validation/FormValidation_MustBeEmail";
import { FormValidation_MustHaveValue } from "@drozdik.m/form/dist/validation/FormValidation_MustHaveValue";
import { FormValidation_MustHaveSameValueAs } from "@drozdik.m/form/dist/validation/formValidation_MustHaveSameValueAs";

declare let lang_passwordNotEmpty: string;
declare let lang_newPasswordNotEmpty: string;
declare let lang_passwordNotSame: string;


WindowEvents.OnDOMReady.Add(function ()
{
    let form = new Form("changePasswordForm");

    let oldPassword = new NativeFormInput(document.getElementById("oldPassword"));
    oldPassword.AddValidation(new FormValidation_MustHaveValue(oldPassword, lang_passwordNotEmpty));

    let newPassword = new NativeFormInput(document.getElementById("newPassword"));
    newPassword.AddValidation(new FormValidation_MustHaveValue(newPassword, lang_newPasswordNotEmpty));

    let newPasswordConfirm = new NativeFormInput(document.getElementById("newPasswordConfirm"));
    newPasswordConfirm.AddValidation(new FormValidation_MustHaveSameValueAs(newPassword, newPasswordConfirm, lang_passwordNotSame));

    form.AddInput(oldPassword);
    form.AddInput(newPassword);
    form.AddInput(newPasswordConfirm);

    form.OnValidSubmit.Add(function ()
    {
        form.Submit();
    });
});