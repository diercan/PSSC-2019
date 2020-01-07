import React, { Component } from 'react';
import {
  Collapse,
  Container, DropdownItem, DropdownMenu, DropdownToggle,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
  UncontrolledDropdown
} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" color="dark" dark>
          <Container fluid={true}>
            <NavbarBrand tag={Link} to="/experiments">ML-Stats</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow mr-auto">
                <NavItem>
                  <NavLink tag={Link} className="" to="/experiments">Experiments</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="" to="/comparer">Comparer</NavLink>
                </NavItem>
                <UncontrolledDropdown>
                  <DropdownToggle caret>
                    Me
                  </DropdownToggle>
                  <DropdownMenu>
                    <DropdownItem header>My stuff</DropdownItem>
                    <DropdownItem>My Experiments</DropdownItem>
                    <DropdownItem>Profile</DropdownItem>
                    <DropdownItem divider />
                    <DropdownItem>Logout</DropdownItem>
                  </DropdownMenu>
                </UncontrolledDropdown>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
