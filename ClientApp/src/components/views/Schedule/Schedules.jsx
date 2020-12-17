import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import ScheduleApi from '../../services/ScheduleApi';

function ScheduleRow(props) {
    const schedule = props.schedule
    const scheduleLink = `/schedule/${schedule.petId}`

    const getBadge = (status) => {
        return status === 'Active' ? 'success' :
            status === 'Inactive' ? 'secondary' :
                status === 'Pending' ? 'warning' :
                    status === 'Banned' ? 'danger' :
                        'primary'
    }

    return (
        <tr key={schedule.schedule.professionalId.toString()}>
            <th scope="row">{schedule.professional.professionalId}</th>
            <td>{schedule.professional.firstName} {schedule.professional.lastName}</td>
            <td>{new Date(schedule.schedule.dateTimeBegin).toLocaleDateString()} {new Date(schedule.schedule.dateTimeBegin).toLocaleTimeString()}</td>
            <td>{new Date(schedule.schedule.dateTimeEnd).toLocaleDateString()} {new Date(schedule.schedule.dateTimeEnd).toLocaleTimeString()}</td>
            <td>
                <Link to={"/schedule/edit/" + schedule.schedule.dateTimeBegin} params={{ schedule: schedule }}>
                    <Button block color="info" size="sm">Edit</Button>
                </Link>
            </td>
            <td>
                <DeleteModal onDelete={() => props.deleteSchedulesHandler(schedule.schedule.dateTimeBegin)} />
            </td>
        </tr>
    )
}

class Schedules extends Component {

    constructor() {
        super();
        this.state = { schedules: [] };
    }

    componentDidMount() {
        document.title = "Schedule";
        this.updateSchedulesHandler();
    }


    updateSchedulesHandler = () => ScheduleApi.getTakenSchedule(
        schedules => this.setState({ schedules: schedules }));

    addSchedulesHandler = (schedule) => ScheduleApi.addSchedule(schedule, this.updateSchedulesHandler);

    editSchedulesHandler = (schedule) => ScheduleApi.editSchedule(schedule, this.updateSchedulesHandler);

    deleteSchedulesHandler = (appointmentId) => ScheduleApi.deleteSchedule(appointmentId,
        this.updateAppointmentsHandler);



    render() {
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xl={8}>
                        <Card>
                            <CardHeader>
                                <i className="fa fa-align-justify"></i> Schedule
                            </CardHeader>
                            <CardBody>
                                <Table responsive hover>
                                    <thead>
                                        <tr>
                                            <th scope="col">Professional's Name</th>
                                            <th scope="col">Begin Date</th>
                                            <th scope="col">End Date</th>
                                            <th scope="col">Edit</th>
                                            <th scope="col">Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.schedules.map((schedule, index) =>
                                            <ScheduleRow key={index} schedule={schedule} editSchedulesHandler={this.editSchedulesHandler } deleteSchedulesHandler={this.deleteSchedulesHandler} />
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



export default Schedules;
