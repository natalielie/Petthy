import Axios from 'axios';

class ScheduleApi {


    static getFreeSchedule = (callback) => {
        Axios.get('api/doctor/getSchedule')
            .then(res => callback(res.data))
            .catch(ScheduleApi.errorHandler);
    }


    static getTakenSchedule = (callback) => {
        Axios.get('api/doctor/getProfessionalSchedule')
            .then(res => callback(res.data))
            .catch(ScheduleApi.errorHandler); 
    }

    // ready
    static addVacantSchedule = (dateTimeBegin, dateTimeEnd, callback) => {
        Axios.post('api/doctor/addVacantDatesToSchedule', dateTimeBegin, dateTimeEnd)
            .then(() => ScheduleApi.getFreeSchedule(callback))
            .catch(ScheduleApi.errorHandler);
    }


    errorHandler = error => console.log(error);

}


export default ScheduleApi;
