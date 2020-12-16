import Axios from 'axios';

class AppointmentApi {


    static getFreeSchedule = (callback) => {
        Axios.get('api/commonProfessional/getSchedule')
            .then(res => callback(res.data))
            .catch(AppointmentApi.errorHandler);
        /*const persons = res.data;
        this.setState({ persons });*/
    }


    static getTakenSchedule = (callback) => {
        Axios.get('api/commonProfessional/getProfessionalSchedule')
            .then(res => callback(res.data))
            .catch(AppointmentApi.errorHandler);
    }

    // ready
    static addVacantSchedule = (dateTimeBegin, dateTimeEnd, callback) => {
        Axios.post('api/commonProfessional/addVacantDatesToSchedule', dateTimeBegin, dateTimeEnd)
            .then(() => AppointmentApi.getFreeSchedule(callback))
            .catch(AppointmentApi.errorHandler);
    }

    static getAllAppointments = (callback) => {
        Axios.get('api/admin/getAllAppointments')
            .then(res => callback(res.data))
            .catch(AppointmentApi.errorHandler);
    }

    static addAppointment = (appointment, callback) => {
        Axios.post('api/commonProfessional', appointment)
            .then(() => AppointmentApi.getAllAppointments(callback))
            .catch(AppointmentApi.errorHandler);
    }


    static editAppointment = (appointment, callback) => {
        let id = appointment.appointmentId;
        delete appointment.appointmentId;
        Axios.put('api/commonProfessional/' + id, appointment)
            .then(() => AppointmentApi.getAllAppointments(callback))
            .catch(AppointmentApi.errorHandler);
    }


    static deleteAppointment = (id, callback) => {
        Axios.delete('api/commonProfessional/' + id)
            .then(() => AppointmentApi.getAllAppointments(callback))
            .catch(AppointmentApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}


export default AppointmentApi;
