import Axios from 'axios';

class ProfessionalApi {


    static getProfessionals = (callback) => {
        Axios.get('api/doctor/getAllProfessionals')
            .then(res => callback(res.data))
            .catch(ProfessionalApi.errorHandler);
    }


    static getProfessional = (id, callback) => {
        Axios.get('api/doctor/getSingleProfessional/' + id)
            .then(res => callback(res.data))
            .catch(ProfessionalApi.errorHandler);
    }


    static addProfessional = (professional, callback) => {
        Axios.post('api/doctors', professional)
            .then(() => ProfessionalApi.getProfessionals(callback))
            .catch(ProfessionalApi.errorHandler);
    }


    static editProfessional = (doctor, callback) => {
        let id = doctor.id;
        delete doctor.id;
        Axios.put('api/doctors/' + id, doctor)
            .then(() => ProfessionalApi.getDoctors(callback))
            .catch(ProfessionalApi.errorHandler);
    }


    static deleteProfessional = (id, callback) => {
        Axios.delete('api/doctors/' + id)
            .then(() => ProfessionalApi.getDoctors(callback))
            .catch(ProfessionalApi.errorHandler);
    }




    static getProfessionalRoles = (id, callback) => {
        Axios.get('api/doctor/getSingleProfessional/' + id)
            .then(res => callback(res.data))
            .catch(ProfessionalApi.errorHandler);
    }

    /*static addProfessionalRole = (professionalRole, callback) => {
        Axios.post('api/doctors', professionalRole)
            .then(() => ProfessionalApi.getProfessionals(callback))
            .catch(ProfessionalApi.errorHandler);
    }*/

    errorHandler = error => console.log(error);

}


export default ProfessionalApi;
