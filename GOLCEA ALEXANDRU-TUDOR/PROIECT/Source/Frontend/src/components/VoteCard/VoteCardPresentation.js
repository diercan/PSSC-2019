import React from "react";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import InputLabel from "@material-ui/core/InputLabel";
// core components
import GridItem from "components/Grid/GridItem.js";
import GridContainer from "components/Grid/GridContainer.js";
import CustomInput from "components/CustomInput/CustomInput.js";
import Button from "components/CustomButtons/Button.js";
import Card from "components/Card/Card.js";
import CardHeader from "components/Card/CardHeader.js";
import CardAvatar from "components/Card/CardAvatar.js";
import CardBody from "components/Card/CardBody.js";
import CardFooter from "components/Card/CardFooter.js";

import avatar from "assets/img/faces/marc.jpg";

const styles = {
  cardCategoryWhite: {
    color: "rgba(255,255,255,.62)",
    margin: "0",
    fontSize: "14px",
    marginTop: "0",
    marginBottom: "0"
  },
  cardTitleWhite: {
    color: "#FFFFFF",
    marginTop: "0px",
    minHeight: "auto",
    fontWeight: "300",
    fontFamily: "'Roboto', 'Helvetica', 'Arial', sans-serif",
    marginBottom: "3px",
    textDecoration: "none"
  }
};

const useStyles = makeStyles(styles);

export default function VoteCardPresentation(props) {
  console.log("PROPS",props);
  const classes = useStyles();
  return (
    <div>
      <GridContainer>
        <GridItem xs={12} sm={12} md={8}>
          <Card>
            <CardHeader color="primary">
              <h4 className={classes.cardTitleWhite}>Poll Questions and Options</h4>
            </CardHeader>
            <CardBody>
              <GridContainer>

                {/* {question.option.map(option => {
                    <h6 className={classes.cardTitle}>{option.name}</h6>
                    })} */}


                {props.poll.questions.map(question => (
                  <GridItem xs={12} sm={12} md={5}>
                    <h3 className={classes.cardCategory}>{question.name}</h3>
                    {question.options.map(option => (
                      <h6 className={classes.cardTitle}>{option.name}</h6>
                    ))}
                  </GridItem>
                ))}

              </GridContainer>
            </CardBody>
            <CardFooter>
              <Button color="primary">Register for this poll</Button>
            </CardFooter>
          </Card>
        </GridItem>
        <GridItem xs={12} sm={12} md={4}>
          <Card profile>
            <CardBody profile>
              <h3 className={classes.cardCategory}>{props.poll.name}</h3>
              <h6 className={classes.cardTitle}>{props.poll.startDate}-{props.poll.endDate}</h6>
              <p className={classes.description}>{props.poll.description} </p>
            </CardBody>
          </Card>
        </GridItem>
      </GridContainer>
    </div>
  );
}

{/* <GridItem xs={12} sm={6} md={4} lg={3}>
<div className={classes.title}>
  <h3>Radio Buttons</h3>
</div>
<div
  className={
    classes.checkboxAndRadio +
    " " +
    classes.checkboxAndRadioHorizontal
  }
>
  <FormControlLabel
    control={
      <Radio
        checked={selectedEnabled === "a"}
        onChange={() => setSelectedEnabled("a")}
        value="a"
        name="radio button enabled"
        aria-label="A"
        icon={
          <FiberManualRecord className={classes.radioUnchecked} />
        }
        checkedIcon={
          <FiberManualRecord className={classes.radioChecked} />
        }
        classes={{
          checked: classes.radio,
          root: classes.radioRoot
        }}
      />
    }
    classes={{
      label: classes.label,
      root: classes.labelRoot
    }}
    label="First Radio"
  />
</div>
<div
  className={
    classes.checkboxAndRadio +
    " " +
    classes.checkboxAndRadioHorizontal
  }
>
  <FormControlLabel
    control={
      <Radio
        checked={selectedEnabled === "b"}
        onChange={() => setSelectedEnabled("b")}
        value="b"
        name="radio button enabled"
        aria-label="B"
        icon={
          <FiberManualRecord className={classes.radioUnchecked} />
        }
        checkedIcon={
          <FiberManualRecord className={classes.radioChecked} />
        }
        classes={{
          checked: classes.radio,
          root: classes.radioRoot
        }}
      />
    }
    classes={{
      label: classes.label,
      root: classes.labelRoot
    }}
    label="Second Radio"
  />
</div>
<div
  className={
    classes.checkboxAndRadio +
    " " +
    classes.checkboxAndRadioHorizontal
  }
>
  <FormControlLabel
    disabled
    control={
      <Radio
        checked={false}
        value="a"
        name="radio button disabled"
        aria-label="B"
        icon={
          <FiberManualRecord className={classes.radioUnchecked} />
        }
        checkedIcon={
          <FiberManualRecord className={classes.radioChecked} />
        }
        classes={{
          checked: classes.radio,
          disabled: classes.disabledCheckboxAndRadio,
          root: classes.radioRoot
        }}
      />
    }
    classes={{
      label: classes.label,
      root: classes.labelRoot
    }}
    label="Disabled Unchecked Radio"
  />
</div>
<div
  className={
    classes.checkboxAndRadio +
    " " +
    classes.checkboxAndRadioHorizontal
  }
>
  <FormControlLabel
    disabled
    control={
      <Radio
        checked={true}
        value="b"
        name="radio button disabled"
        aria-label="B"
        icon={
          <FiberManualRecord className={classes.radioUnchecked} />
        }
        checkedIcon={
          <FiberManualRecord className={classes.radioChecked} />
        }
        classes={{
          checked: classes.radio,
          disabled: classes.disabledCheckboxAndRadio,
          root: classes.radioRoot
        }}
      />
    }
    classes={{ label: classes.label, root: classes.labelRoot }}
    label="Disabled Checked Radio"
  />
</div>
</GridItem> */}