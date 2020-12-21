import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import { Route, Switch } from "react-router";
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import authService from 'C:/Users/N/FinalPetthy/ClientApp/src/components/api-authorization/AuthorizeService';

import DeleteModal from './DeleteModal';
import AppointmentApi from '../../services/AppointmentApi';
import AddAppointment from './AddAppointment';


function AppointmentRow(props) {
    const appointment = props.appointment


    return (
        <tr key={appointment.appointmentId.toString()}>
            <th scope="row">{appointment.pet.petName}</th>
            <td>{appointment.professional.firstName} {appointment.professional.lastName}</td>
            <td>{new Date(appointment.dateTimeBegin).toLocaleDateString()} {new Date(appointment.dateTimeBegin).toLocaleTimeString()}</td>
            <td>
                <Link to={"/appointments/edit/" + appointment.appointmentId} params={{ appointment: appointment }}>
                    <Button block color="info" size="sm">Edit</Button>
                </Link>
            </td>
            <td>
                <DeleteModal onDelete={() => props.deleteAppointmentsHandler(appointment.appointmentId)} />
            </td>
        </tr>
    )
}

class Appointments extends Component {

    constructor() {
        super();
        this.state = {
            appointments: [],
            isAdmin: false
        };
    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
        document.title = "Appointments";
        if (username == "admin@gmail.com") {
            this.updateAppointmentsAdminHandler();
        }
        else {
            this.updateAppointmentsHandler(user.sub);

        }
    }

    //for admin
    updateAppointmentsAdminHandler = () => AppointmentApi.getAllAppointments(
        appointments => this.setState({ appointments: appointments, isAdmin: true }));

    editAppointmentsAdminHandler = (appointment) => AppointmentApi.editAppointment(
        appointment, this.updateAppointmentsAdminHandler);

    deleteAppointmentsAdminHandler = (appointmentId) => AppointmentApi.deleteAppointment(
        appointmentId,
        this.updateAppointmentsAdminHandler);

    //for users
    updateAppointmentsHandler = () => AppointmentApi.getMyAppointments(
        appointments => this.setState({ appointments: appointments }));

    addAppointmentsHandler = (appointment) => AppointmentApi.addAppointment(
        appointment, this.updateAppointmentsHandler);

    editAppointmentsHandler = (appointment) => AppointmentApi.editAppointment(
        appointment, this.updateAppointmentsHandler);

    deleteAppointmentsHandler = (appointmentId) => AppointmentApi.deleteAppointment(
        appointmentId,
        this.updateAppointmentsHandler);



    render() {

        if (this.state.isAdmin) {
            return (
                <div className="animated fadeIn">
                    <Row>
                        <Col xl={8}>
                            <Card>
                                <CardHeader>
                                    <i className="fa fa-align-justify"></i> Appointments
                            </CardHeader>
                                <CardBody>
                                    <Table responsive hover>
                                        <thead>
                                            <tr>
                                                <th scope="col">Pet Name</th>
                                                <th scope="col">Professional's Name</th>
                                                <th scope="col">Date</th>
                                                <th scope="col">Edit</th>
                                                <th scope="col">Delete</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {this.state.appointments.map((appointment, index) =>
                                                <AppointmentRow key={index} appointment={appointment} editAppointmentsAdminHandler={this.editAppointmentsAdminHandler} deleteAppointmentsAdminHandler={this.deleteAppointmentsAdminHandler} />
                                            )}
                                        </tbody>
                                    </Table >
                                </CardBody >
                            </Card >
                        </Col >
                    </Row >
                    <Link to={"/appointments/add/"}>
                        <Button onClick={AddAppointment} block color="info" size="sm">Add new appointment</Button>
                    </Link>
                </div >
                

            )    
        }

        if (this.state.isAdmin == false) {
            return (
                
                <div className="animated fadeIn">
                    <Row>
                        <Col xl={8}>
                            <Card>
                                <CardHeader>
                                    <i className="fa fa-align-justify"></i> Appointments
                            </CardHeader>
                                <CardBody>
                                    <Table responsive hover>
                                        <thead>
                                            <tr>
                                                <th scope="col">Pet Name</th>
                                                <th scope="col">Professional's Name</th>
                                                <th scope="col">Date</th>
                                                <th scope="col">Edit</th>
                                                <th scope="col">Delete</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {this.state.appointments.map((appointment, index) =>
                                                <AppointmentRow key={index} appointment={appointment} editAppointmentsHandler={this.editAppointmentsHandler} deleteAppointmentsHandler={this.deleteAppointmentsHandler} />
                                            )}
                                        </tbody>
                                    </Table >
                                </CardBody >
                            </Card >
                        </Col >
                    </Row >

                   <Button>
                        <Link tag={Link} className="text-dark" to="/appointments-add/" > Add a new appointment</Link>
                    </Button>
                </div >

            )    
        }

    }
}

export default Appointments;

