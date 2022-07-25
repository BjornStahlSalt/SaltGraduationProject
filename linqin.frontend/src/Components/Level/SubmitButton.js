import * as React from 'react';
import Box from '@mui/material/Box';
import CircularProgress from '@mui/material/CircularProgress';
import { green, red } from '@mui/material/colors';
import Button from '@mui/material/Button';
import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';

export default function TestButton({ submitAnswer, compileError, loading }) {
    const timer = React.useRef();

    const buttonSx = {
        ...(compileError == 'Correct!!' && {
            bgcolor: green[500],
            '&:hover': {
                bgcolor: green[700],

            },
        }),
        ...(compileError == 'Wrong Answer!' && {
            bgcolor: red[500],
            '&:hover': {
                bgcolor: red[700],

            },
        }),
    };

    React.useEffect(() => {
        return () => {
            clearTimeout(timer.current);
        };
    }, []);
    const handleButtonClick = () => {
        submitAnswer();
    };
    return (
        <Box sx={ { display: 'flex', alignItems: 'center' } }>
            <Box sx={ { m: 1, position: 'relative' } }>
                <Button
                    variant="contained"
                    sx={ buttonSx }
                    disabled={ loading }
                    onClick={ handleButtonClick }
                >
                    Submit Answer
                </Button>
                { loading && (
                    <CircularProgress
                        size={ 24 }
                        sx={ {
                            color: green[500],
                            position: 'absolute',
                            top: '50%',
                            left: '50%',
                            marginTop: '-12px',
                            marginLeft: '-12px',
                        } }
                    />
                ) }
            </Box>
        </Box>
    );
}