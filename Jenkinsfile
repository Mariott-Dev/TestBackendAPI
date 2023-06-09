pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo "Building dotnet backend.."
                bat "dotnet restore $TestBackendAPI.sln"
            }
        }
        stage('Test') {
            steps {
                echo "Testing.."
                sh '''
                echo "Enter Test Protocols Here."
                '''
            }
        }
        stage('Deliver') {
            steps {
                echo 'Deliver....'
                sh '''
                echo "Enter Delivery Proticols Here."
                '''
            }
        }
    }
}
