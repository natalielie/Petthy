import React, { Component } from 'react';
import { Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import MedCardApi from '../../services/MedCardApi';

class PetMedNote extends Component {

    constructor() {
        super();

        this.state = { petMedNote: {} };
        
        this.getPetMedNoteHandler = this.getPetMedNoteHandler.bind(this);
    }

    componentDidMount() {
        document.title = "PetMedCard";        
        this.getPetMedNoteHandler(this.props.match.params.professionalId);
    }

    getPetMedNoteHandler = (petId) => MedCardApi.getMedCardData(
        petId, petMedNote => this.setState({ petMedNote: petMedNote }));


    render() {

        return (
            <div className="animated fadeIn">
                <Row>
                    <Col lg={6}>
                        <Card>
                            <CardHeader>
                                <strong><i className="icon-info pr-1"></i>Pet id: {this.props.match.params.petMedCardNoteId}</strong>
                            </CardHeader>
                            <CardBody>
                                <Table responsive striped hover>
                                    <tbody>
                                        <tr>
                                            <td>{`PetId:`}</td>
                                            <td><strong>{this.state.assignment.petId}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Pet Name:`}</td>
                                            <td><strong>{this.state.petMedNote.petName}</strong></td>
                                        </tr>
                                        
                                        <tr>
                                            <td>{`Illness:`}</td>
                                            <td><strong>{this.state.assignment.illness}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Treatment:`}</td>
                                            <td><strong>{this.state.assignment.Treatment}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Comment:`}</td>
                                            <td><strong>{this.state.assignment.Comment}</strong></td>
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

export default PetMedNote;
