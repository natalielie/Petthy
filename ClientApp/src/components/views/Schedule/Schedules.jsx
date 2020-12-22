import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import ScheduleApi from '../../services/ScheduleApi';
import { useTranslation } from 'react-i18next';
import { withTranslation } from 'react-i18next';
import i18next from "i18next";

function ScheduleRow(props) {
    const schedule = props.schedule
    const scheduleLink = `/schedule/${schedule.petId}`
    const { t, i18n } = useTranslation();
    var dateFormat = require("dateformat");

    if (i18next.language == "en") {
        
        return (

            <tr key={schedule.professional.professionalId.toString()}>
                <th scope="row">{schedule.professional.firstName} {schedule.professional.lastName}</th>
                <td>{dateFormat(schedule.dateTimeBegin, "yyyy/mm/dd")} {dateFormat(schedule.dateTimeBegin, "h:MM TT")}</td>
                <td>{dateFormat(schedule.dateTimeEnd, "yyyy/mm/dd")} {dateFormat(schedule.dateTimeEnd, "h:MM TT")}</td>
                <td>
                    <Link to={"/schedule/edit/" + schedule.dateTimeBegin} params={{ schedule: schedule }}>
                        <Button block color="info" size="sm">{t("Edit")}</Button>
                    </Link>
                </td>
                <td>
                    <DeleteModal onDelete={() => props.deleteSchedulesHandler(schedule.dateTimeBegin)} />
                </td>
            </tr>
        )
    }
    else {
        return (
            <tr key={schedule.professional.professionalId.toString()}>
                <th scope="row">{schedule.professional.firstName} {schedule.professional.lastName}</th>
                <td>{dateFormat(schedule.dateTimeBegin, "dd-mm-yyyy")} {dateFormat(schedule.dateTimeBegin, "HH:MM")}</td>
                <td>{dateFormat(schedule.dateTimeEnd, "dd-mm-yyyy")}  {dateFormat(schedule.dateTimeEnd, "HH:MM")}</td>
                <td>
                    <Link to={"/schedule/edit/" + schedule.dateTimeBegin} params={{ schedule: schedule }}>
                        <Button block color="info" size="sm">{t("Edit")}</Button>
                    </Link>
                </td>
                <td>
                    <DeleteModal onDelete={() => props.deleteSchedulesHandler(schedule.dateTimeBegin)} />
                </td>
            </tr>
        )
    }
    
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


    updateSchedulesHandler = () => ScheduleApi.getFreeSchedule(
        schedules => this.setState({ schedules: schedules }));

    addSchedulesHandler = (schedule) => ScheduleApi.addSchedule(schedule, this.updateSchedulesHandler);

    editSchedulesHandler = (schedule) => ScheduleApi.editSchedule(schedule, this.updateSchedulesHandler);

    deleteSchedulesHandler = (appointmentId) => ScheduleApi.deleteSchedule(appointmentId,
        this.updateAppointmentsHandler);



    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xl={8}>
                        <Card>
                            <CardHeader>
                                <i className="fa fa-align-justify"></i> {t("My Schedule")}
                            </CardHeader>
                            <CardBody>
                                <Table responsive hover>
                                    <thead>
                                        <tr>
                                            <th scope="col">{t("ProfessionalsName")}</th>
                                            <th scope="col">{t("Begin Date")}</th>
                                            <th scope = "col" > {t("End Date")}</th>
                                            <th scope="col">{t("Edit")}</th>
                                            <th scope="col">{t("Delete")}</th>
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



export default withTranslation()(Schedules);
