pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo "Building dotnet backend.."
            }
        }
        stage('Test') {
            steps {
                echo "Testing.."
                sh '''
                echo "Enter Test Protocols Here (for API)."
                '''
            }
        }
        stage('Deliver') {
            steps {
                echo 'Deliver....'
                sh '''
                echo "Enter Delivery Protcols Here (for API)."
                '''
            }
        }
    }
}
