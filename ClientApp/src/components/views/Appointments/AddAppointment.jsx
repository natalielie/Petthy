import React, { Component, useState } from 'react';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import AppointmentApi from '../../services/AppointmentApi';
import Datetime from 'react-datetime';
import { withTranslation } from "react-i18next";



const ComboboxProfessional = (props) => {

    const onChange = e => {
        const { name, value } = e.target;
    }
    const {
        current_professional
    } = 0;

    const professionalOptions = [
        {
            label: "John Smith",
            value: 1,
        },
        {
            label: "Kate Wolson",
            value: 2,
        },
        {
            label: "Anatolii Kompotov",
            value: 3,
        },
        {
            label: "Ahmad Amshanov",
            value: 4,
        },
        {
            label: "Troye Bull",
            value: 5,
        },
    ];

    return (
        <>
            <div className="form-group">
                <select value={current_professional} onChange={onChange} id="license_type" name="current_professional">
                    {professionalOptions.map((option) => (
                        <option value={option.value}>{option.label}</option>
                    ))}
                </select>
            </div>
        </>
    )
}

const ComboboxPet = (props) => {

    const onChange = e => {
        const { name, value } = e.target;
    }
    const {
        current_pet
    } = 0;

    const petOptions = [
        {
            label: "Twinkle",
            value: 1,
        },
        {
            label: "Jim",
            value: 2,
        },
        {
            label: "Cinnabon",
            value: 3,
        },
    ];

    return (
        <>
            <div className="form-group">
                <select value={current_pet} onChange={onChange} id="license_type" name="current_pet">
                    {petOptions.map((option) => (
                        <option value={option.value}>{option.label}</option>
                    ))}
                </select>
            </div>
        </>
    )
}


class AddAppointment extends Component {

    constructor() {
        super();

        this.addAppointmentHandler = this.addAppointmentHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        document.title = "Add Appointment";
    }

    addAppointmentHandler = (appointment, callback) => AppointmentApi.addAppointment(appointment, callback);

    handleSubmit = (event) => {
        event.preventDefault();
        var appdate = new Date("12/24/2020T11:00:00");
        var data = {
            name: 1,
            pet: 1,
            date: appdate
        };
        
        this.addAppointmentHandler(data, () => this.props.history.push('/appointments/'));
    }

    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>{t("Add a new appointment")}</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="professional">{t("ProfessionalsName")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <ComboboxProfessional />
                                        </Col>
                                    </FormGroup>

                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="pet">{t("PetName")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <ComboboxPet />
                                        </Col>
                                    </FormGroup>

                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="date">{t("Date and time")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Datetime />;
                                        </Col>
                                    </FormGroup>
                                    <Button type="submit" color="primary">{t("Submit")}</Button>
                                </form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}


export default withTranslation()(AddAppointment);
