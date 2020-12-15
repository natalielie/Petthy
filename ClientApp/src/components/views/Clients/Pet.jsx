import React, { Component } from 'react';
import { Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import PetApi from '../../services/PetApi';
import utils from '../../utils'
import patientsTemplate from './PetsTemplate';

class Pet extends Component {

    constructor() {
        super();

        this.state = { pet: {} };
        
        this.getPetHandler = this.getPetHandler.bind(this);
    }

    componentDidMount() {
        document.title = "Pet";        
        this.getPetHandler(this.props.match.params.petId);
    }
    
    getPetHandler = (petId) => PetApi.getPet(id, pet => this.setState({pet: pet}));


    render() {

        return (
            <div className="animated fadeIn">
                <Row>
                    <Col lg={6}>
                        <Card>
                            <CardHeader>
                                <strong><i className="icon-info pr-1"></i>Pet id: {this.props.match.params.petId}</strong>
                            </CardHeader>
                            <CardBody>
                                <Table responsive striped hover>
                                    <tbody>
                                        {
                                            patientsTemplate.map(prop =>                                                
                                                <tr key={prop.petName}>
                                                    <td>{`${prop.display || utils.capitalize(prop.petName)}:`}</td>
                                                    <td><strong>{utils.stringify(this.state.pet[prop.petName])}</strong></td>
                                                </tr>
                                            )
                                        }
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

export default Pet;
