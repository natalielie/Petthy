import Axios from 'axios';

class ClientsApi {


    static getClients = (callback) => {
        Axios.get('api/client/getAllClients')
            .then(res => callback(res.data))
            .catch(ClientsApi.errorHandler);
    }


    static getClient = (id, callback) => {
        Axios.get('api/client/getSingleClient/' + id)
            .then(res => callback(res.data))
            .catch(ClientsApi.errorHandler);
    }


    /*static addUser = (patient, callback) => {
        Axios.post('api/client', patient)
            .then(() => UsersApi.getUsers(callback))
            .catch(UsersApi.errorHandler);
    }


    static editUser = (user, callback) => {
        let id = user.id;
        delete user.id;
        Axios.put('api/users/' + id, user)
            .then(() => UsersApi.getUsers(callback))
            .catch(UsersApi.errorHandler);
    }


    static deleteUser = (id, callback) => {
        Axios.delete('api/users/' + id)
            .then(() => UsersApi.getUsers(callback))
            .catch(UsersApi.errorHandler);
    }*/


    errorHandler = error => console.log(error);

}


export default ClientsApi;
