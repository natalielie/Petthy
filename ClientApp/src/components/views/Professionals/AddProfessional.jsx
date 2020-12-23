import React, { Component } from 'react';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row } from 'reactstrap';
import DoctorsApi from '../../services/ProfessionalApi';

class AddProfessional extends Component {

    constructor() {
        super();

        this.addProfessionalHandler = this.addProfessionalHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        document.title = "Add Professional";
    }

    addProfessionalHandler = (professional, callback) => ProfessionalApi.addProfessional(professional, callback);

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            name: event.target.elements['name'].value
        };
        
        this.addProfessionalHandler(data, () => this.props.history.push('/professionals'));
    }

    render() {
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>Add Professional</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="name">Name</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="name"  name="name" required placeholder="Name" />
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

export default AddProfessional;
