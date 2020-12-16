import Axios from 'axios';

class PetApi {


    static getPets = (callback) => {
        Axios.get('api/pet/getAllPets')
            .then(res => callback(res.data))
            .catch(PetApi.errorHandler);
    }

  
    static getPet = (id, callback) => {
        Axios.get('api/pet/getSinglePet' + id)
            .then(res => callback(res.data))
            .catch(PetApi.errorHandler);
    }


    static getPetsAndOwners = (callback) => {
        Axios.get('api/pet/getAllPetsAndOwners')
            .then(res => callback(res.data))
            .catch(PetApi.errorHandler);
    }

   /* static getPetAndOwner = (callback) => {
        Axios.all([Axios.get(`api/pet/getAllPets`),
        Axios.get(`api/client/getAllClients`)])
            .then(Axios.spread((firstResponse, secondResponse) =>
                callback(firstResponse.data, secondResponse.data)))
            .catch(PetApi.errorHandler);
    }*/

    /*static admitPatient = (id, callback) => {
        Axios.get('api/patients/' + id + '/admit')
            .then(res => callback(res.data))
            .catch(PatientsApi.errorHandler);
    }


    static dischargePatient = (id, callback) => {
        Axios.get('api/patients/' + id + '/discharge')
            .then(res => callback(res.data))
            .catch(PatientsApi.errorHandler);
    }


    static addPatient = (patient, callback) => {
        Axios.post('api/patients', patient)
            .then(() => PatientsApi.getPatients(callback))
            .catch(PatientsApi.errorHandler);
    }


    static editPatient = (patient, callback) => {
        let id = patient.id;
        delete patient.id;
        Axios.put('api/patients/' + id, patient)
            .then(() => PatientsApi.getPatients(callback))
            .catch(PatientsApi.errorHandler);
    }


    static deletePatient = (id, callback) => {
        Axios.delete('api/patients/' + id)
            .then(() => PatientsApi.getPatients(callback))
            .catch(PatientsApi.errorHandler);
    }*/

    getPetByClient = (clientId, callback) => {
        Axios.get('api/pet/getSinglePetByClient' + clientId)
            .then(res => callback(res.data))
            .catch(PetApi.errorHandler);
    }


    errorHandler = error => console.log(error);

}


export default PetApi;
