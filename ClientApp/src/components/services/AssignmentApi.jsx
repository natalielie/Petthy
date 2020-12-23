﻿import Axios from 'axios';

class AssignmentApi {


    static getMyAssignments = (callback) => {
        Axios.get('api/doctor/getMyAssignments/')
            .then(res => callback(res.data))
            .catch(AssignmentApi.errorHandler);
    }

    static getAllAssignments = (callback) => {
        Axios.get('api/admin/getAllAssignments')
            .then(res => callback(res.data))
            .catch(AssignmentApi.errorHandler);
    }

    static getSpecificAssignment = (petId, callback) => {
        Axios.get('api/commonProfessional/getSpecificAssignment/' + petId)
            .then(res => callback(res.data))
            .catch(AssignmentApi.errorHandler);
    }


    /*static addAssignment = (petId, callback) => {
        Axios.post('api/commonProfessional/assignPetToDoctor', petId)
            .then(() => AssignmentApi.getAllAssignments(callback))
            .catch(AssignmentApi.errorHandler);
    }*/


    /*static deleteAssignment = (petId, callback) => {
        Axios.delete('api/commonProfessional/DeletePetAssignment/' + petId)
            .then(() => AssignmentApi.getAllAssignments(callback))
            .catch(AssignmentApi.errorHandler);
    }*/

    static addAssignmentAdmin = (petId, professionalId, callback) => {
        Axios.post('api/admin/assignPetToDoctor', {
            data: {
                petId: petId,
                professionalId: professionalId
            }
        })
            .then(() => AssignmentApi.getAllAssignments(callback))
            .catch(AssignmentApi.errorHandler);
    }


    static addAssignment = (petId, callback) => {
        Axios.post('api/doctor/assignPetToDoctor', {
            data: {
                petId: petId
            }
        })
            .then(() => AssignmentApi.getAllAssignments(callback))
            .catch(AssignmentApi.errorHandler);
    }


    static deleteAssignment = (petId, professionalId, callback) => {
        Axios.delete('api/doctor/DeletePetAssignment/',
            {
                data: {
                    petId: petId,
                    professionalId: professionalId
                }
            })
            .then(() => AssignmentApi.getAllAssignments(callback))
            .catch(AssignmentApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}


export default AssignmentApi;
