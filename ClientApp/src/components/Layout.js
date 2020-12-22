import React, { Component } from 'react';
import { Container } from 'reactstrap';
import  NavMenu  from './NavMenu';
import { withTranslation } from "react-i18next";

const NavMenuComponent = withTranslation()(NavMenu);


export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
            <NavMenuComponent />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
