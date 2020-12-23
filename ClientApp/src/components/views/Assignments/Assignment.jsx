import React, { Component } from 'react';
import { Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import ProfessionalApi from '../../services/AssignmentApi';

class Assignment extends Component {

    constructor() {
        super();

        this.state = { assignment: {} };
        
        this.getAssignmentHandler = this.getAssignmentHandler.bind(this);
    }

    componentDidMount() {
        document.title = "Assignment";        
        this.getAssignmentHandler(this.props.match.params.professionalId);
    }

    getAssignmentHandler = (professionalId) => AssignmentlApi.getMyAssignment(
        professionalId, assignment => this.setState({ assignment: assignment }));


    render() {

        return (
            <div className="animated fadeIn">
                <Row>
                    <Col lg={6}>
                        <Card>
                            <CardHeader>
                                <strong><i className="icon-info pr-1"></i>Professional id: {this.props.match.params.professionalId}</strong>
                            </CardHeader>
                            <CardBody>
                                <Table responsive striped hover>
                                    <tbody>
                                        
                                        <tr>
                                            <td>{`ID:`}</td>
                                            <td><strong>{this.state.assignment.professionalId}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`PetId:`}</td>
                                            <td><strong>{this.state.assignment.petId}</strong></td>
                                        </tr>
                                        {/* {
                                            patientsTemplate.map(prop =>                                                
                                                <tr key={prop.name}>
                                                    <td>{`${prop.display || utils.capitalize(prop.name)}:`}</td>
                                                    <td><strong>{utils.stringify(this.state.patient[prop.name])}</strong></td>
                                                </tr>
                                            )
                                        } */}
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

export default Assignment;
