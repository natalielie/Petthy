import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import AppointmentApi from '../../services/AppointmentApi';

function AppointmentRow(props) {
    const appointment = props.appointment
    const appointmentLink = `/appointments/${appointment.pet.petId}`

    const getBadge = (status) => {
        return status === 'Active' ? 'success' :
            status === 'Inactive' ? 'secondary' :
                status === 'Pending' ? 'warning' :
                    status === 'Banned' ? 'danger' :
                        'primary'
    }

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
        this.state = { appointments: [] };
    }

    componentDidMount() {
        document.title = "Appointments";
        this.updateAppointmentsHandler();
    }


    updateAppointmentsHandler = () => AppointmentApi.getAllAppointments(
        appointments => this.setState({ appointments: appointments }));

    addAppointmentsHandler = (appointment) => AppointmentApi.addAppointment(appointment, this.updateAppointmentsHandler);

    editAppointmentsHandler = (appointment) => AppointmentApi.editAppointment(appointment, this.updateAppointmentsHandler);

    deleteAppointmentsHandler = (appointmentId) => AppointmentApi.deleteAppointment(appointmentId,
        this.updateAppointmentsHandler);



    render() {
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
                                            <AppointmentRow key={index} appointment={appointment} editAppointmentsHandler={this.editAppointmentsHandler } deleteAppointmentsHandler={this.deleteAppointmentsHandler} />
                                        )}
                                    </tbody>
                                </Table>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}



export default Appointments;
