export class Constants {
    static readonly DATE_FORMAT_BR = 'dd/MM/yyyy';
    static readonly DATE_TIME_FORMAT_BR = `${this.DATE_FORMAT_BR} HH:mm`;
    static readonly MIN_PHONE_LENGTH = 10;
    static readonly PASSWORD_MIN_LENGTH = 8;
    static readonly PASSWORD_MAX_LENGTH = 16;
    static readonly MIN_NAME_LENGTH = 3;
    static readonly MAX_NAME_LENGTH = 30;
    static readonly MIN_USER_LENGTH = 5;
    static readonly MAX_USER_LENGTH = 25;
    static readonly PASSWORD_PATTERN = `^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+={}\\[\\]|\\\\:;"'<>,.?/])[0-9a-zA-Z!@#$%^&*()_+={}\\[\\]|\\\\:;"'<>,.?/]{${this.PASSWORD_MIN_LENGTH},${this.PASSWORD_MAX_LENGTH}}$`;
    static readonly PASSWORD_SPECIAL_CHARS = '!@#$%^&*()_+={}[]|\\:;"\'<>,.?/';
    static readonly PASSWORD_PATTERN_ERROR_MSG = '<p>Senha deve ter o seguinte formato:</p>' + 
                                                 '<ul>' +
                                                 '<li>Pelo menos uma letra maiúscula</li>' +
                                                 '<li>Pelo menos uma letra minúscula</li>' +
                                                 '<li>Pelo menos um número</li>' +
                                                 '<li>Pelo menos um caractere especial</li>' +
                                                 '<li>'+`${this.PASSWORD_SPECIAL_CHARS}`+'</li>' +
                                                 '<li>Não pode ter espaços em branco</li>' +
                                                 '<li>Ter entre 8 e 16 caracteres</li>' +
                                                 '</ul>'; 
}
