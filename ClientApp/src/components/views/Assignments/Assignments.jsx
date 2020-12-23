import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from 'C:/Users/N/FinalPetthy/ClientApp/src/components/api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import AssignmentApi from '../../services/AssignmentApi';
import { useTranslation } from 'react-i18next';


import { withTranslation } from "react-i18next";

function AssignmentRow(props) {
    const assignment = props.assignment
    const assignmentLink = `/pet/${assignment.pet.petId}`
    const { t, i18n } = useTranslation();

        return (
            <tr key={assignment.professional.professionalId.toString()}>
                <th scope="row"><Link to={assignmentLink}>{assignment.pet.petName}</Link></th>
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
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xl={8}>
                        <Card>
                            <CardHeader>
                                <i className="fa fa-align-justify"></i> {t("Assignments")}
                            </CardHeader>
                            <CardBody>
                                <Table responsive hover>
                                    <thead>
                                        <tr>
                                            <th scope="col">{t("PetName")}</th>
                                            <th scope="col">{t("ProfessionalsName")}</th>
                                            <th scope="col">{t("Terminate")}</th>
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
                <Button class="btn btn-primary" style={{ marginTop: 20 }}>
                    <Link tag={Link} className="text-dark" to="/assignments-add/" >{t("Add Assignment")}</Link>
                </Button>
            </div>
        )
    }
}



export default withTranslation()(Assignments);
