import React, { Component } from 'react';
import { Link } from 'react-router-dom';
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
                <td>{assignment.professional.firstName}</td>
                <td>{assignment.professional.lastName}</td>
                <td>
                    <DeleteModal onDelete={() => props.deleteAssignmentHandler(assignment)} />
                </td>
            </tr>
        )
    }

class Assignments extends Component {

    constructor() {
        super();
        this.state = { assignments: [] };
    }

    componentDidMount() {
        document.title = "Assignments";
        this.updateAssignmentsHandler();
    }

    // for admin
    updateAssignmentsHandler = () => AssignmentApi.getAllAssignments(assignments => this.setState({ assignments: assignments }));

   // addAssignmentsAdminHandler = (professionalId, petId) => AssignmentApi.addAssignmentAdmin(assignment, this.updateAssignmentsHandler);

    addAssignmentsHandler = (assignment) => AssignmentApi.addAssignment(
        assignment, this.updateAssignmentsHandler);

    //deleteAssignmentsAdminHandler = (petId, professionalId) => AssignmentApi.addAssignmentAdmin(petId, professionalId,
       // this.updateAssignmentsHandler);

    deleteAssignmentsHandler = (assignment) => AssignmentApi.addAssignmentAdmin(assignment, this.updateAssignmentsHandler);


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
                                            <th scope="col">Id</th>
                                            <th scope="col">Pet Name</th>
                                            <th scope="col">Professional's Name</th>
                                            <th scope="col"></th>
                                            <th scope="col">Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.assignments.map((assignment, index) =>
                                            <AssignmentRow key={index} assignment={assignment} deleteAssignmentsHandler={this.deleteAssignmentsHandler} />
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
