import React, { Component } from 'react';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row } from 'reactstrap';
import DoctorsApi from '../../services/ProfessionalApi';
import Professional from './Professional';

class EdiProfessional extends Component {

    constructor() {
        super();

        this.state = { professional: {} };

        this.getProfessionalHandler = this.getProfessionalHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        document.title = "Edit Professional";
        this.getProfessionalHandler(this.props.match.params.id);
    }

    editProfessionalHandler = (professional, callback) => ProfessionalApi.editProfessional(professional, callback);

    getProfessionalHandler = (id) => ProfessionalApi.getProfessional(id, professional => this.setState({ professional: professional }));

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            id: this.state.professional.professionalId,
            name: event.target.elements['name'].value
        };
        
        console.log(data)

        this.editProfessionalHandler(data, () => this.props.history.push('/professionals'));
    }

    render() {
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>Edit Professional</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="name">First Name</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="name" placeholder="Name" required defaultValue={this.state.professional.firstName} />
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

export default EditProfessional;
