import { InputTypes } from './InputTypes';

const petsTemplate = [
    { name: 'petId', display: 'Id', type: InputTypes.NUMBER, readOnly: false, required: true },
    { name: 'petName', type: InputTypes.TEXT, readOnly: false, required: true },
    { name: 'animalKind', type: InputTypes.TEXT, readOnly: false, required: true },
    { name: 'petSex', type: InputTypes.TEXT, readOnly: false, required: true },
    // { name: 'bill', type: InputTypes.TEXT, readOnly: true, required: false },
    { name: 'clientId', display: 'Client Id', type: InputTypes.NUMBER, readOnly: false, required: false },
    { name: 'clientFirstName', type: InputTypes.TEXT, readOnly: false, required: true },
    { name: 'clientLastName', type: InputTypes.TEXT, readOnly: false, required: true },
   // { name: 'nurseId', display: 'Nurse ID', type: InputTypes.NUMBER, readOnly: false, required: false },
    //{ name: 'roomId', display: 'Room ID', type: InputTypes.NUMBER, readOnly: false, required: false },
    // { name: 'userName', type: InputTypes.TEXT, readOnly: false, required: true },
    // { name: 'password', type: InputTypes.PASSWORD, readOnly: false, required: true }
];

export default petsTemplate
