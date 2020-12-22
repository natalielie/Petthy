import i18next from "i18next";
import { initReactI18next } from "react-i18next";
import HttpApi from "i18next-http-backend";

import { withTranslation } from 'react-i18next';


import i18n from "i18next";

i18n
    .use(initReactI18next) 
    .init({
        resources: {
            en: {
                translations: {

                    Selectlanguage: "Select language",

                    "Health System": "Health System",
                    "This is Petthy Health System": "This is Petthy Health System",
                    "The health system, which provides the necessary and proper care for your pet":
                        "The health system, which provides the necessary and proper care for your pet and cares for their health",

                    Home: "Home",
                    Name: "Name",
                    ProfessionalRole: "Professional Role",
                    Edit: "Edit",
                    Delete: "Delete",
                    Veterinerian: "Veterinerian",
                    Specialist: "Specialist",
                    Professionals: "Professionals",
                    Workplace: "Workplace",

                    Pets: "Pets",
                    PetName: "PetName",
                    Kind: "Kind",
                    Gender: "Gender",
                    "Pet Owner Name": "Pet Owner's Name",

                    ProfessionalsName: "Professional's Name",
                    Date: "Date",
                    Appointments: "Appointments",
                    "Add a new appointment": "Add a new appointment",
                    "Begin Date": "Begin Date",
                    "End Date": "End Date",
                    
                    Assignments: "Assignments",
                    Terminate: "Terminate",

                    "My Assignments": "My Assignments",
                    "My Appointments": "My Appointments",
                    "My Schedule": "My Schedule",

                    "Add Assignment": "Add Assignment",
                    Submit: "Submit",
                    "Date and time": "Date and time",

                    Hello: "Hello",
                    Register: "Register",
                    Login: "Login",
                    Logout: "Logout",

                    Confirmation: "Confirmation",
                    Cancel: "Cancel",
                    "Are you sure?": "Are you sure?"
                },
            },
            ua: {
                translations: {
                    Selectlanguage: "Оберіть мову",

                    "Health System": "Оздоровча система",
                    "This is Petthy Health System": "Це Petthy оздоровча система",
                    "The health system, which provides the necessary and proper care for your pet":
                        "Оздоровча система, що надає необхідний і правильний догляд за вашими тваринами та піклується про їх здоров'я",

                    Home: "Головна",
                    Name: "Ім'я",
                    ProfessionalRole: "Посада",
                    Edit: "Відредагувати",
                    Delete: "Видалити",
                    Veterinerian: "Ветеринар",
                    Specialist: "Спеціаліст з догляду",
                    Professionals: "Спеціалісти",
                    Workplace: "Місце роботи",

                    Pets: "Тварини",
                    PetName: "Ім'я тварини",
                    Kind: "Вид",
                    Gender: "Стать",
                    "Pet Owner Name": "Ім'я господаря",

                    ProfessionalsName: "Ім'я спеціаліста",
                    Date: "Дата",
                    Appointments: "Прийоми лікаря",
                    "Add a new appointment": "Новий прийом",
                    "Begin Date": "Початок",
                    "End Date": "Кінець",
                    
                    Assignments: "Договри",
                    Terminate: "Розірвати договір",

                    "My Assignments": "Мої договори",
                    "My Appointments": "Мої прийоми",
                    "My Schedule": "Мій розклад",

                    "Add Assignment": "Заключити договір",
                    Submit: "Підтвердити",
                    "Date and time": "Дата та час",

                    Hello: "Привіт,",
                    Register: "Зареєструватися",
                    Login: "Увійти",
                    Logout: "Вийти",

                    Confirmation: "Підтвердження",
                    Cancel: "Відмінити",
                    "Are you sure?": "Ви впевнені?"

                },
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