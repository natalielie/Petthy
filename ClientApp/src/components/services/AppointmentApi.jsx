import Axios from 'axios';

class AppointmentApi {


    static getFreeSchedule = (callback) => {
        Axios.get('api/commonProfessional/getSchedule')
            .then(res => callback(res.data))
            .catch(ScheduleApi.errorHandler);
        /*const persons = res.data;
        this.setState({ persons });*/
    }


    static getTakenSchedule = (callback) => {
        Axios.get('api/commonProfessional/getProfessionalSchedule')
            .then(res => callback(res.data))
            .catch(ScheduleApi.errorHandler);
    }

    // ready
    static addVacantSchedule = (dateTimeBegin, dateTimeEnd, callback) => {
        Axios.post('api/commonProfessional/addVacantDatesToSchedule', dateTimeBegin, dateTimeEnd)
            .then(() => ScheduleApi.getFreeSchedule(callback))
            .catch(ScheduleApi.errorHandler);
    }


    errorHandler = error => console.log(error);

}


export default ScheduleApi;
