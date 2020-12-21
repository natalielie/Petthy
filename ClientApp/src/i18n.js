import i18next from "i18next";
import { initReactI18next } from "react-i18next";
import HttpApi from "i18next-http-backend";

import { withTranslation } from 'react-i18next';


import i18n from "i18next";

i18n.init({
    resources: {
        en: {
            translations: {
                Selectlanguage: "Select language",

                Home: "Home",
                Name: "Name",
                ProfessionalRole: "Professional Role",
                Edit: "Edit",
                Delete: "Delete",
                Veterinerian: "Veterinerian",
                Specialist: "Specialist",
                Professionals: "Professionals",
                Workplace: "Workplace",
            },
        },
    },
        ua: {
            translations: {
                Selectlanguage: "Оберіть мову",

                Home: "Головна",
                Name: "Ім'я",
                ProfessionalRole: "Посада",
                Edit: "Відредагувати",
                Delete: "Видалити",
                Veterinerian: "Ветеринар",
                Specialist: "Спеціаліст з догляду",
                Professionals: "Спеціалісти",
                Workplace: "Місце роботи"
            },
        },
        

    fallbackLng: "en",
    debug: true,

    // have a common namespace used around the full app
    ns: ["translations"],
    defaultNS: "translations",

    keySeparator: false, // we use content as keys

    interpolation: {
        escapeValue: false, // not needed for react!!
        formatSeparator: ","
    },

    react: {
        wait: true
    }
});

export default i18n;