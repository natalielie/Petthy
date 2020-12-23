import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UserManager, WebStorageStateStore } from 'oidc-client';
import authService from 'C:/Users/N/FinalPetthy/ClientApp/src/components/api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import MedCardApi from '../../services/MedCardApi';
import { useTranslation } from 'react-i18next';


import { withTranslation } from "react-i18next";

function PetMedNoteRow(props) {
    const petMedNote = props.petMedNote
    const assignmentLink = `/medcard/${petMedNote.petMedCardNoteId}`
    const { t, i18n } = useTranslation();
    var dateFormat = require("dateformat");

        return (
            <tr key={petMedNote.petMedCardNoteId.toString()}>
                <th scope="row"><Link to={assignmentLink}>{petMedNote.petName}</Link></th>
                <td>{petMedNote.illness}</td>
                <td>{petMedNote.treatment}</td>
                <td>{petMedNote.Comment}</td>
                <td>{dateFormat(petMedNote.noteDate, "yyyy/mm/dd")}</td>
            </tr>
        )
    }

class PetMedNotes extends Component {

    constructor() {
        super();
        this.state = { petMedNotes: [] };

    }

    async componentDidMount() {
        this.updateMedCardHandler();

    }

    // for admin
    updateMedCardAdminHandler = () => MedCardApi.getMedCardData(
        assignments => this.setState({ assignments: assignments }));

    addMedCardAdmin = (assignment) => MedCardApi.addNoteToMedCard(
        assignment, this.updateMedCardAdminHandler);
    
    //deleteMedCardHandler = (petId, professionalId) => MedCardApi.deleteAssignment(
       // petId, professionalId, this.updateMedCardAdminHandler);

    //for user
    updateMedCardHandler = () => MedCardApi.getMedCardData(
        petMedNotes => this.setState({ petMedNotes: petMedNotes }));

    addMedCardHandler = (petMedNote) => MedCardApi.addNoteToMedCard(
        petMedNote, this.updateMedCardHandler);



    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xl={8}>
                        <Card>
                            <CardHeader>
                                <i className="fa fa-align-justify"></i> Pet Medcard
                            </CardHeader>
                            <CardBody>
                                <Table responsive hover>
                                    <thead>
                                        <tr>
                                            <th scope="col">{t("PetName")}</th>
                                            <th scope="col">Illness</th>
                                            <th scope="col">Treatment</th>
                                            <th scope="col">Comment</th>
                                            <th scope="col">Note date</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.petMedNotes.map((petMedNote, index) =>
                                            <PetMedNoteRow key={index} petMedNote={petMedNote} />
                                        )}
                                    </tbody>
                                </Table>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
                <Button class="btn btn-primary" style={{ marginTop: 20 }}>
                    <Link tag={Link} className="text-dark" to="/petMedNote-add/" >{t("Add a new note")}</Link>
                </Button>
            </div>
        )
    }
}



export default withTranslation()(PetMedNotes);
