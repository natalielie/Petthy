import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Card, CardBody, CardHeader, Col, Row, Table, Button } from 'reactstrap';
import PetApi from '../../services/PetApi';
import utils from '../../utils'
import petsTemplate from './PetsTemplate';
import { withTranslation } from "react-i18next";


class Pet extends Component {

    constructor() {
        super();

        this.state = { pet: {} };
        
        this.getPetHandler = this.getPetHandler.bind(this);
    }

    componentDidMount() {
        document.title = "Pet";        
        this.getPetHandler();
    }
    
    getPetHandler = () => PetApi.getPet(pet => this.setState({pet: pet}));


    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col lg={6}>
                        <Card>
                            <CardHeader>
                                <strong><i className="icon-info pr-1"></i>Pet name: Twinkie</strong>
                            </CardHeader>
                            <CardBody>
                                <Table responsive striped hover>
                                    <tbody>{/*
                                        petsTemplate.map(prop =>
                                            <tr key={prop.petName}>
                                                <td>{`${prop.display || utils.capitalize(prop.clientFirstName)}:`}</td>
                                                <td><strong>{utils.stringify(this.state.pet.owner[prop.clientFirstName])}</strong></td>
                                            </tr>
                                        )
                                    */}
                                            <tr key={this.state.pet.petName}></tr>
                                            <tr>{t("Pet Owner Name")}</tr>
                                            <tr><strong>Tyler Joseph</strong></tr>
                                            <tr>{t("Kind")}</tr>
                                            <tr><strong>Cat</strong></tr>
                                            <tr>{t("Gender")}</tr>
                                            <tr><strong>Female</strong></tr>
                                                
                                        
                                    </tbody>
                                </Table>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
                <Button class="btn btn-primary" style={{ marginTop: 20 }}>
                    <Link tag={Link} className="text-dark" to="/medcard/" >{t("Pet's medcard")}</Link>
                </Button>
                <Button class="btn btn-primary" style={{ marginTop: 20, marginLeft: 20 }}>
                    <Link tag={Link} className="text-dark" to="/appointments-add/" >{t("Pet's diary")}</Link>
                </Button>
            </div>
        )
    }
}

export default withTranslation()(Pet);
