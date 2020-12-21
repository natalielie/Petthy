import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from 'C:/Users/N/FinalPetthy/ClientApp/src/components/api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import AssignmentApi from '../../services/AssignmentApi';

function AssignmentRow(props) {
    const assignment = props.assignment
    const assignmentLink = `/assignments/${assignment.pet.petId}`

    const getBadge = (status) => {
        return status === 'Active' ? 'success' :
            status === 'Inactive' ? 'secondary' :
                status === 'Pending' ? 'warning' :
                    status === 'Banned' ? 'danger' :
                        'primary'
    }

        return (
            <tr key={assignment.professional.professionalId.toString()}>
                <th scope="row">{assignment.pet.petName}</th>
                <td>{assignment.professional.firstName} {assignment.professional.lastName}</td>
                <td>
                    <DeleteModal onDelete={() => props.deleteAssignmentHandler(
                        assignment.pet.petId, assignment.professional.professionalId)} />
                </td>
            </tr>
        )
    }

class Assignments extends Component {

    constructor() {
        super();
        this.state = { assignments: [] };

    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
        document.title = "Assignments";
        if (username == "admin@gmail.com") {
            this.updateAssignmentsAdminHandler();
        }
        else {
            this.updateAssignmentsHandler(user.sub);

        }
    }

    // for admin
    updateAssignmentsAdminHandler = () => AssignmentApi.getAllAssignments(
        assignments => this.setState({ assignments: assignments }));

    addAssignmentAdmin = (assignment) => AssignmentApi.addAssignment(
        assignment, this.updateAssignmentsAdminHandler);

    deleteAssignmentHandler = (petId, professionalId) => AssignmentApi.deleteAssignment(
        petId, professionalId, this.updateAssignmentsAdminHandler);

    //for user
    updateAssignmentsHandler = () => AssignmentApi.getMyAssignments(
        assignments => this.setState({ assignments: assignments }));

    addAssignmentsHandler = (assignment) => AssignmentApi.addAssignment(
        assignment, this.updateAssignmentsHandler);

    deleteAssignmentHandler = (petId, professionalId) => AssignmentApi.deleteAssignment(
        petId, professionalId, this.updateAssignmentsHandler);


    render() {
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xl={8}>
                        <Card>
                            <CardHeader>
                                <i className="fa fa-align-justify"></i> Assignments
                            </CardHeader>
                            <CardBody>
                                <Table responsive hover>
                                    <thead>
                                        <tr>
                                            <th scope="col">Pet Name</th>
                                            <th scope="col">Professional's Name</th>
                                            <th scope="col">Terminate</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.assignments.map((assignment, index) =>
                                            <AssignmentRow key={index} assignment={assignment} deleteAssignmentHandler={this.deleteAssignmentHandler} />
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



export default Assignments;
