import React, { Component } from 'react';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row } from 'reactstrap';
import ScheduleApi from '../../services/ScheduleApi';

class AddVacantDates extends Component {

    constructor() {
        super();

        this.addScheduleHandler = this.addScheduleHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        document.title = "Add Vacant Dates";
    }

    addScheduleHandler = (dates, callback) => ScheduleApi.addVacantDates(dates, callback);

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            begin: event.target.elements['begin'].value
            end: event.target.elements['end'].value
        };
        
        this.addScheduleHandler(data, () => this.props.history.push('/schedule'));
    }

    render() {
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>Add Patient</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="begin">Begin Date</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="begin" name="begin" required placeholder="Begin date" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="end">End Date</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="end" name="end" required placeholder="End date" />
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

export default AddDoctor;
