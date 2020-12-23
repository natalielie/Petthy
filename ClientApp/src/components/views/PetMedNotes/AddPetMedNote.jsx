import React, { Component, useState } from 'react';
import { withTranslation } from 'react-i18next';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import MedCardApi from '../../services/MedCardApi';


class AddMedNote extends Component {

    constructor() {
        super();

        this.addMedCardHandler = this.addMedCardHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        document.title = "Add a new note";
    }

    addMedCardHandler = (medNote, callback) => MedCardApi.addNoteToMedCard(medNote, callback);

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            illness: event.target.elements['illness'].value,
            treatment: event.target.elements['treatment'].value,
            treatment: event.target.elements['comment'].value
        };

        this.addMedCardHandler(data, () => this.props.history.push('/medcard/'));
    }

    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>Add a new note</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                        <FormGroup row>
                                            <Col md="3">
                                                <Label htmlFor="illness">Illness</Label>
                                            </Col>
                                            <Col xs="12" md="9">
                                            <Input type="text" id="illness" required placeholder="Illness" />
                                            </Col>
                                        </FormGroup>
                                        <FormGroup row>
                                            <Col md="3">
                                            <Label htmlFor="treatment">Treatment</Label>
                                            </Col>
                                            <Col xs="12" md="9">
                                            <Input type="text" id="treatment" placeholder="Treatment" />
                                            </Col>
                                        </FormGroup>
                                        <FormGroup row>
                                            <Col md="3">
                                            <Label htmlFor="comment">Comment</Label>
                                            </Col>
                                            <Col xs="12" md="9">
                                            <Input type="text" id="comment" placeholder="Comment" />
                                            </Col>
                                        </FormGroup>
                                        
                                    <Button type="submit" color="primary">{t("Submit")}</Button>
                                </form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}


export default withTranslation()(AddMedNote);
