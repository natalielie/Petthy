import Axios from 'axios';

class BackupApi {


    static backupAdmin = (callback) => {
        Axios.post('api/admin/backupDatabase/')
            .then(() => AssignmentApi.getAllAssignments(callback))
            .catch(BackupApi.errorHandler);
    }


    static restoreAdmin = (callback) => {
        Axios.post('api/doctor/restoreDatabase', {
            data: {
                petId: petId
            }
        })
            .then(() => AssignmentApi.getAllAssignments(callback))
            .catch(BackupApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}
