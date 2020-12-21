import React, { Component, useState } from 'react';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import AppointmentApi from '../../services/AppointmentApi';
import Datetime from 'react-datetime';



const ComboboxProfessional = (props) => {

    const [dropdownOpen, setDropdownOpen] = useState(false);

    const toggle = () => setDropdownOpen(prevState => !prevState);
    return (
        <Dropdown>
            <DropdownToggle caret>
        </DropdownToggle>
            <DropdownMenu>
                <DropdownItem>John Smith</DropdownItem>
                <DropdownItem>Kate Wolson</DropdownItem>
                <DropdownItem>Anatolii Kompotov</DropdownItem>
                <DropdownItem>Ahmad Ahmed</DropdownItem>
            </DropdownMenu>
        </Dropdown>
    );
}

const ComboboxPet = (props) => {

    const [dropdownOpen, setDropdownOpen] = useState(false);

    const toggle = () => setDropdownOpen(prevState => !prevState);
    return (
        <Dropdown >
            <DropdownToggle caret>
            </DropdownToggle>
            <DropdownMenu>
                <DropdownItem>John Smith</DropdownItem>
                <DropdownItem>Kate Wolson</DropdownItem>
                <DropdownItem>Anatolii Kompotov</DropdownItem>
                <DropdownItem>Ahmad Ahmed</DropdownItem>
            </DropdownMenu>
        </Dropdown>
    );
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

        var data = {
            name: event.target.elements['professional'].value,
            pet: event.target.elements['pet'].value,
            date: event.target.elements['date'].value
        };
        
        this.addAppointmentHandler(data, () => this.props.history.push('/appointments'));
    }

    render() {
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>Add Appointment</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="professional">Professional Name</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            {ComboboxProfessional}
                                        </Col>
                                    </FormGroup>

                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="pet">Pet Name</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            {ComboboxPet}
                                        </Col>
                                    </FormGroup>

                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="date">Date and time</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Datetime />;
                                        </Col>
                                    </FormGroup>
                                    <Button type="submit" color="primary">Submit</Button>
                                </form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}


/*class ComboboxProfessional extends React.Component {
    professionals = ["John Smith", "Kate Wolson", "Anatolii Kompotov", "Ahmad Ahmed"];
    state = {
        allowCustom: true
    };

    onChange = (event) => {
        this.setState({
            allowCustom: event.target.checked
        });
    }

    render() {
        return (
            <ComboBox data={this.professionals}  />
        );
    }
}

class ComboboxPet extends React.Component {
    pets = ["Twinkle", "Jim", "Cinnabon"];
    state = {
        allowCustom: true
    };

    onChange = (event) => {
        this.setState({
            allowCustom: event.target.checked
        });
    }

    render() {
        return (
            <ComboBox data={this.pets} />

        );
    }
}*/

/*ReactDOM.render(
    <AppComponent />,
    document.querySelector('Petthy')
);*/

export default AddAppointment;
