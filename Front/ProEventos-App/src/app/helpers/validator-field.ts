import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { Constants } from "@app/util/constants";

export class ValidatorField {

    static MustMatch(controlName: string, matchingControlName: string): ValidatorFn {
        return (formGroup: AbstractControl): ValidationErrors | null => {
            const control = formGroup.get(controlName);
            const matchingControl = formGroup.get(matchingControlName);
    
            if (matchingControl?.errors && !matchingControl.errors['mustMatch']) {
                return null;
            }
    
            if (control?.value !== matchingControl?.value) {
                matchingControl?.setErrors({ mustMatch: true });
                return { mustMatch: true };
            } else {
                matchingControl?.setErrors(null);
                return null;
            }
        };
    }
    
    static getErrorMessage(fieldName: string, validatorName: string, validatorValue?: number): string {
        const config: Record<string, string> = {
        'required': `${fieldName} é obrigatório!`,
        'minlength': `${fieldName} precisa ter no mínimo ${validatorValue} caracteres!`,
        'maxlength': `${fieldName} precisa ter no máximo ${validatorValue} caracteres!`,
        'email': `E-mail inválido!`,
        'mustMatch': `${fieldName} não confere!`,
        'pattern': `Caractere inválido!`,
        'patternPwd': Constants.PASSWORD_PATTERN_ERROR_MSG,
        'requiredtrue': `Aceite os termos!`
        };
    
        return config[validatorName];
    }

}