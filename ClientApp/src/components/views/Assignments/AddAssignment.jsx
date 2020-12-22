import React, { Component, useState } from 'react';
import { withTranslation } from 'react-i18next';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import AssignmentApi from '../../services/AssignmentApi';


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


class AddAssignment extends Component {

    constructor() {
        super();

        this.addAppointmentHandler = this.addAppointmentHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        document.title = "Add Assignment";
    }

    addAppointmentHandler = (appointment, callback) => AssignmentApi.addAssignment(appointment, callback);

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            name: event.target.elements['professional'].value,
            pet: event.target.elements['pet'].value
        };

        this.addAppointmentHandler(data, () => this.props.history.push('/assignments'));
    }

    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>{t("Add Assignment")}</strong>
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


export default withTranslation()(AddAssignment);
