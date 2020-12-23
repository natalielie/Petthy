import Axios from 'axios';

class AppointmentApi {

    static getAllAppointments = (callback) => {
        Axios.get('api/admin/getAllAppointments')
            .then(res => callback(res.data))
            .catch(AppointmentApi.errorHandler);
    }

    static getMyAppointments = (callback) => {
        Axios.get('api/doctor/getProfessionalSchedule')
            .then(res => callback(res.data))
            .catch(AppointmentApi.errorHandler);
    }

    static addAppointment = (appointment, callback) => {
        Axios.post('api/doctor/addAppointment/', appointment)
            .then(() => AppointmentApi.getAllAppointments(callback))
            .catch(AppointmentApi.errorHandler);
    }


    static editAppointment = (appointment, callback) => {
        let id = appointment.appointmentId;
        delete appointment.appointmentId;
        Axios.put('api/doctor/changeAppointment/' + id, appointment)
            .then(() => AppointmentApi.getAllAppointments(callback))
            .catch(AppointmentApi.errorHandler);
    }


    static deleteAppointment = (id, callback) => {
        Axios.delete('api/doctor/deleteAppointment' + id)
            .then(() => AppointmentApi.getAllAppointments(callback))
            .catch(AppointmentApi.errorHandler);
    }

    errorHandler = error => console.log(error);

}


export default AppointmentApi;
