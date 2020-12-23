import Axios from 'axios';

class MedCardApi {


    static getMedCardData = (callback) => {
        Axios.get('api/doctor/getMedNote')
            .then(res => callback(res.data))
            .catch(MedCardApi.errorHandler);
    }


    static addNoteToMedCard = (mednote, callback) => {
        Axios.post('api/doctor/addMedNote', mednote)
            .then(() => MedCardApi.getMedCardData(callback))
            .catch(MedCardApi.errorHandler);
    }


    errorHandler = error => console.log(error);

}


export default MedCardApi;
