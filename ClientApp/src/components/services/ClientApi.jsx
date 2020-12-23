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

    errorHandler = error => console.log(error);

}


export default ClientsApi;
